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
    public class LookUps
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
        public void TheLookUpsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin", "pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/Admin/Home");
            driver.FindElement(By.LinkText("Commodity Type")).Click();
            // Warning: assertTextPresent may require manual changes
            driver.WaitForHttpResponse(By.LinkText("Create Commodity Type"));
            driver.FindElement(By.LinkText("Create Commodity Type")).Click();
            driver.WaitForHttpResponse(By.Id("Name"));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Sample Comodity");
            driver.WaitForHttpResponse(By.XPath("//button[@type='button']"));
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            //driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[3]"));
            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[3]")).Click();
            // Warning: assertTextPresent may require manual changes
           // Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
           // driver.WaitForHttpResponse(By.CssSelector("input[type=\"submit\"]"));
           // driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Commodity")).Click();  
            driver.FindElement(By.LinkText("Sub Commodities")).Click();
            driver.FindElement(By.LinkText("Parent Commodities")).Click();
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div[3]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("uniqueId")).Click();
            driver.WaitForHttpResponse(By.XPath("(//button[@type='button'])[2]"));
            driver.FindElement(By.XPath("(//button[@type='button'])[2]")).Click();
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div[3]/div/ul/li")).Click();
            driver.WaitForHttpResponse(By.LinkText("Blended food"));
            driver.FindElement(By.LinkText("Blended food")).Click();
            driver.WaitForHttpResponse(By.XPath("(//button[@type='button'])[4]"));
            driver.FindElement(By.XPath("(//button[@type='button'])[4]")).Click();
            driver.FindElement(By.LinkText("Sub Commodities")).Click();
            driver.FindElement(By.XPath("//div[@id='TabStrip-2']/form/table/tbody/tr/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='TabStrip-2']/form/table/tbody/tr/td[4]/div/div/span")).Click();
            //driver.WaitForHttpResponse(By.XPath("//div[6]/div/ul/li[3]"));
            //driver.FindElement(By.XPath("//div[6]/div/ul/li[3]")).Click();
            //driver.FindElement(By.XPath("//div[@id='TabStrip-2']/form/table/tbody/tr/td[4]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//div[6]/div/ul/li[4]")).Click();
            //driver.FindElement(By.XPath("//div[@id='TabStrip-2']/form/table/tbody/tr/td[2]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//div[7]/div/ul/li[2]")).Click();
            //driver.FindElement(By.XPath("//div[@id='TabStrip-2']/form/table/tbody/tr/td[4]/div/div/span/span")).Click();
            //driver.WaitForHttpResponse(By.XPath("//div[6]/div/ul/li[2]"));
            //driver.FindElement(By.XPath("//div[6]/div/ul/li[2]")).Click();
            //driver.FindElement(By.XPath("//div[@id='TabStrip-2']/form/table/tbody/tr/td[4]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//div[6]/div/ul/li[3]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Program")).Click();
            Thread.Sleep(1000);
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*List of Programs[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Create New Program")).Click();
            driver.WaitForHttpResponse(By.Id("Name"));
            
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Sample Program");
            driver.WaitForHttpResponse(By.Id("Description"));
            
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("description");
            driver.WaitForHttpResponse(By.XPath("//button[@type='button']"));
           
            driver.FindElement(By.XPath("//button[@type='button']")).Click();


            //driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[3]"));
            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[3]")).Click();
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
            //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
           
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Donor")).Click();
            driver.FindElement(By.LinkText("Create New Donor")).Click();
            driver.WaitForHttpResponse(By.Id("Name"));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Sample Donor");
            driver.WaitForHttpResponse(By.Id("IsResponsibleDonor"));
            driver.FindElement(By.Id("IsResponsibleDonor")).Click();
            driver.WaitForHttpResponse(By.Id("IsSourceDonor"));
            driver.FindElement(By.Id("IsSourceDonor")).Click();
            driver.WaitForHttpResponse(By.XPath("//button[@type='button']"));
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[12]"));
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[12]")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Are you sure you want to delete this[\\s\\S][\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='body']/fieldset/div[5]")));
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("input[type=\"submit\"]"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Unit")).Click();
            Thread.Sleep(1000);
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
            driver.FindElement(By.Id("Name")).SendKeys("Sample Unit");
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            driver.WaitForHttpResponse("(//a[contains(text(),'Delete')])[6]");
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[6]")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Commodity Grade")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*List Of Commodity Grades[\\s\\S]*$"));
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
            driver.FindElement(By.Id("Name")).SendKeys("sample Grade");
            driver.FindElement(By.Id("SortOrder")).Clear();
            driver.FindElement(By.Id("SortOrder")).SendKeys("2");
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            //driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[5]"));
           // driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[5]")).Click();
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
           // Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Are you sure you want to delete this[\\s\\S][\\s\\S]*$"));
            //driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Commodity Source")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*List Of Commodity Sources[\\s\\S]*$"));
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.WaitForHttpResponse(By.Id("Name"));
            driver.FindElement(By.Id("Name")).Clear();
            driver.FindElement(By.Id("Name")).SendKeys("Sample Source");
            driver.FindElement(By.XPath("//button[@type='button']")).Click();

            //driver.WaitForHttpResponse("(//a[contains(text(),'Delete')])[9]");
            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[9]")).Click();
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Are you sure you want to delete this[\\s\\S][\\s\\S]*$"));
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
