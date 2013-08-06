using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
//using Selenium;
using CatsUnitTest;

namespace SeleniumTests
{
    [TestFixture]
    public class Authentication
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private FirefoxProfile profile;
        
        [SetUp]
        public void SetupTest()
        {
            
            profile = new FirefoxProfile();
            profile.SetPreference("Browser.link.open_newwindow", 3);
            driver = new FirefoxDriver(profile);
            //driver.Manage().Window.Maximize();
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
            //Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheAuthenticationTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            Assert.AreEqual("Commodity Tracking System : DRMFSS Application", driver.Title);
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "CommodityTracking System Welcome to the DRMFSS Food tracking system"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.AreEqual("Log On", driver.FindElement(By.LinkText("Log On")).Text);
            driver.FindElement(By.LinkText("Log On")).Click();
            Thread.Sleep(3000);
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Log On"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Username"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Password"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Remember me"));
            Assert.IsTrue(IsElementPresent(By.Id("UserName")));
            Assert.IsTrue(IsElementPresent(By.Id("Password")));
            driver.FindElement(By.Id("RememberMe")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("RememberMe")));
            driver.FindElement(By.Id("RememberMe")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("input.t-button")));
            Assert.IsTrue(IsElementPresent(By.XPath("//input[@value='Cancel']")));
            //Boolean  = IsElementPresent(By.LinkText("Unable to login?"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Unable to login?")));
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("admin");
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("admin");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            Thread.Sleep(3000);
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "Login was unsuccessful"));
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("pass2pass");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            // Warning: assertTextPresent may require manual changes
            Thread.Sleep(3000);
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
