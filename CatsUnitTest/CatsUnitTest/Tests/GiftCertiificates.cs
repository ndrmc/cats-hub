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
    public class GiftCertiificates
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
           // Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheGiftCertiificatesTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/GiftCertificate/Create");
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Gift Date[\\s\\S]*$"));
           // driver.FindElement(By.LinkText("7")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("input.t-button")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Add new record")));
           // Assert.IsTrue(IsElementPresent(By.LinkText("Delete")));
            //Assert.IsTrue(IsElementPresent(By.LinkText("Back to List")));
            Assert.AreEqual("Commodity Tracking System : Gift Certificate", driver.Title);
            Assert.IsTrue(IsElementPresent(By.LinkText("Log Off")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Settings")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Welcome Admin User ! Log Off | Settings[\\s\\S]*$"));
            Assert.AreEqual("Home", driver.FindElement(By.LinkText("Home")).Text);
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Mode Of transport[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Program[\\s\\S]*$"));
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Donor[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity Type[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.IsTrue(IsElementPresent(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")));
            
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*CommodityTracking System(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)Welcome to the DRMFSS Food tracking system\\. This application is developed by the help of WFP to assist with the tracking of Food and non food commodities\\. Any news article that you have to be aware of will be displayed here\\.[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Thread.Sleep(3000);
            //driver.FindElement(By.LinkText("Add New Gift Certificate")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*New Gift Certificate(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)Use this form to create new or edit gift certificates\\. [\\s\\S]* marks required fields\\. [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Gift Certificate Headers[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Gift Date [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Mode Of transport [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Program [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Donor [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity Type [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Thread.Sleep(3000);
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/div[2]/table/tbody/tr/td/div[6]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[5]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/div[2]/table/tbody/tr/td/div[8]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//li[14]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/div[2]/table/tbody/tr/td/div[10]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[7]/div/ul/li")).Click();
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("1234560");
            driver.FindElement(By.Id("PortName")).Clear();
            driver.FindElement(By.Id("PortName")).SendKeys("Dijibuti");
            driver.FindElement(By.Id("ReferenceNo")).Clear();
            driver.FindElement(By.Id("ReferenceNo")).SendKeys("963258");
            driver.FindElement(By.Id("Vessel")).Clear();
            driver.FindElement(By.Id("Vessel")).SendKeys("ET458");
            driver.FindElement(By.CssSelector("span.t-icon.t-icon-calendar")).Click();
            driver.FindElement(By.LinkText("6")).Click();
            driver.FindElement(By.LinkText("Add new record")).Click();
            driver.WaitForHttpResponse(By.XPath("//div[8]/div/ul/li[2]"));
            driver.FindElement(By.XPath("//div[8]/div/ul/li[2]")).Click();
            Thread.Sleep(3000);
           
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            Thread.Sleep(1000);
            //driver.FindElement(By.LinkText("Edit")).Click();
            //WaitForHttpRequest();
            //// Warning: verifyTextPresent may require manual changes
            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Edit Gift Certificate(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)Use this form to create new or edit gift certificates\\. [\\s\\S]* marks required fields\\.[\\s\\S]*$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}
            //driver.FindElement(By.XPath("//div[@id='body']/form/div[2]/table/tbody/tr/td/div[8]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//li[16]")).Click();
            //driver.FindElement(By.CssSelector("input.t-button")).Click();
            //WaitForHttpRequest();
            driver.Navigate().GoToUrl(baseURL + "/LetterTemplate");
            Thread.Sleep(1000);
            //driver.FindElement(By.LinkText("Manage Letter Templates")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Gift Certificate Letter Templates(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)Please use this form to create / Manage gift certificate letter templates\\. Note that the gift certificate templates are prepared one page at a time\\. [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Create New")).Click();
            Thread.Sleep(1000);
            driver.Navigate().GoToUrl(baseURL +"/GiftCertificate");
            driver.Navigate().GoToUrl(baseURL + "/GiftCertificate/MonthlySummary");
           // driver.FindElement(By.LinkText("Monthly Summary")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Monthly Gift Certificate Report(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)This page shows the amount of commodity DRMFSS has received a gift certificate for\\.[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }

          
            //driver.FindElement(By.LinkText("Gift Date")).Click();
            //driver.FindElement(By.LinkText("Monthly Chart")).Click();
            //// Warning: verifyTextPresent may require manual changes
            //try
            //{
            //    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Monthly Gift Certificate Chart(\n|\r\n)(\n|\r\n)(\n|\r\n)(\n|\r\n)This page shows the amount of commodity DRMFSS has received a gift certificate for\\. [\\s\\S]*$"));
            //}
            //catch (AssertionException e)
            //{
            //    verificationErrors.Append(e.Message);
            //}
            //driver.FindElement(By.LinkText("Gift Date")).Click();

            driver.Navigate().GoToUrl(baseURL);
           
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
