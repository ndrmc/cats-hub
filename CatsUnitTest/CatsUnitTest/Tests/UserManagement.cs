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
    public class UserManagement
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
        public void TheUserManagementTest()
        {

            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/Admin/Home");

            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*User Accounts And System Administration[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Mange Roles")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Mange Users")));
            Assert.IsTrue(IsElementPresent(By.XPath("(//a[contains(text(),'Change Password')])[2]")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Hub Managment[\\s\\S]*$"));
            //Boolean  = IsElementPresent(By.LinkText("Hub Owners"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Hub")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Store")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Transporter and Others[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Transporters")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Translations[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Translations")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Transactions[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity and Related Lookups[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Administrative Locations And Final Distribution points[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Commodity Type")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Commodity")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Program")));
           // Boolean  = IsElementPresent(By.LinkText("Donor"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Unit")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Commodity Grade")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Commodity Source")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Administrative Locations And Final Distribution points[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Manage Admin Units")));
            Assert.IsTrue(IsElementPresent(By.LinkText("FDPs")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Error Log")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Transactions[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.LinkText("Journal")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Ledger")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*System Errors and Exceptions[\\s\\S]*$"));
            driver.FindElement(By.LinkText("Mange Roles")).Click();
            driver.WaitForHttpResponse(By.LinkText("Create New"));
            driver.FindElement(By.LinkText("Create New")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Name[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Description[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SortOrder[\\s\\S]*$"));
            //Boolean  = IsElementPresent(By.Id("Name"));
            Assert.IsTrue(IsElementPresent(By.Id("Name")));
            Assert.IsTrue(IsElementPresent(By.Id("Description")));
            Assert.IsTrue(IsElementPresent(By.Id("SortOrder")));
            //Boolean  = IsElementPresent(By.XPath("//button[@type='button']"));
            Assert.IsTrue(IsElementPresent(By.XPath("//button[@type='button']")));
            Assert.IsTrue(IsElementPresent(By.XPath("(//button[@type='button'])[2]")));
            driver.WaitForHttpResponse(By.Id("Name"));
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
            driver.FindElement(By.Id("Name")).SendKeys("Sample Role");
            driver.WaitForHttpResponse(By.Id("Description"));
            driver.FindElement(By.Id("Description")).Clear();
            driver.FindElement(By.Id("Description")).SendKeys("Sample Role Description");
            driver.WaitForHttpResponse(By.Id("SortOrder"));
            driver.FindElement(By.Id("SortOrder")).Clear();
            driver.FindElement(By.Id("SortOrder")).SendKeys("1");
            driver.WaitForHttpResponse(By.XPath("//button[@type='button']"));
            driver.FindElement(By.XPath("//button[@type='button']")).Click();
            //driver.WaitForHttpResponse(By.XPath("(//a[contains(text(),'Delete')])[2]"));
            driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[2]")).Click();
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete Role[\\s\\S][\\s\\S]*$"));
            driver.WaitForHttpResponse(By.CssSelector("input[type=\"submit\"]"));
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.WaitForHttpResponse(By.LinkText("Admin"));
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.LinkText("Mange Users")).Click();
            // Warning: assertTextPresent may require manual changes
            driver.WaitForHttpResponse(By.LinkText("Create New"));
            driver.FindElement(By.LinkText("Create New")).Click();
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("div.ui-widget-overlay"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            Assert.IsTrue(IsElementPresent(By.CssSelector("div.ui-widget-overlay")));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*First Name[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Last Name[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Grand Father's Name[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Email address[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Password[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Confirm password[\\s\\S]*$"));
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.CssSelector("div.ui-widget-overlay"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            ////driver.WaitForHttpResponse(By.XPath("(//input[@id='UserName'])[2]"));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='UserName'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='FirstName'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='LastName'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='GrandFatherName'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='Email'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='Password'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//input[@id='ConfirmPassword'])[2]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//button[@type='button'])[3]")));
            //Assert.IsTrue(IsElementPresent(By.XPath("(//button[@type='button'])[4]")));
            driver.WaitForHttpResponse(By.Id("UserName"));
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys("Fisseha");
            driver.WaitForHttpResponse(By.Id("FirstName"));
            driver.FindElement(By.Id("FirstName")).Clear();
            driver.FindElement(By.Id("FirstName")).SendKeys("Fisseha");
            driver.WaitForHttpResponse(By.Id("LastName"));
            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("Abebe");
            driver.WaitForHttpResponse(By.Id("GrandFatherName"));
            driver.FindElement(By.Id("GrandFatherName")).Clear();
            driver.FindElement(By.Id("GrandFatherName")).SendKeys("Chari");
            driver.WaitForHttpResponse(By.Id("Email"));
            driver.FindElement(By.Id("Email")).Clear();
            driver.FindElement(By.Id("Email")).SendKeys("abebefisseha5@gmail.com");
            driver.WaitForHttpResponse(By.Id("Password"));
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys("123456");
            driver.WaitForHttpResponse(By.Id("Password"));
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("123456");
            
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
            for (int second = 0;; second++) {
                if (second >= 60) Assert.Fail("timeout");
                try
                {
                    if (IsElementPresent(By.XPath("(//a[contains(text(),'Delete')])[9]"))) break;
                }
                catch (Exception)
                {}
                Thread.Sleep(1000);
            }
            //driver.FindElement(By.XPath("(//a[contains(text(),'Delete')])[9]")).Click();
            // Warning: assertTextPresent may require manual changes
            //Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Delete[\\s\\S]*$"));
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
            driver.FindElement(By.XPath("(//a[contains(text(),'Change Password')])[2]")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Change Password[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("OldPassword")).Clear();
            driver.FindElement(By.Id("OldPassword")).SendKeys("pass2pass");
            driver.FindElement(By.Id("OldPassword")).Clear();
            driver.FindElement(By.Id("OldPassword")).SendKeys("");
            driver.FindElement(By.Id("OldPassword")).Clear();
            driver.FindElement(By.Id("OldPassword")).SendKeys("pass2pass");
            driver.FindElement(By.Id("NewPassword")).Clear();
            driver.FindElement(By.Id("NewPassword")).SendKeys("pass2pass1");
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("pass2pass1");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Change Password\r(\n|\r\n)\r(\n|\r\n)Your password has been changed successfully\\. [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("Admin")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Change Password')])[2]")).Click();
            driver.FindElement(By.Id("OldPassword")).Clear();
            driver.FindElement(By.Id("OldPassword")).SendKeys("pass2pass1");
            driver.FindElement(By.Id("NewPassword")).Clear();
            driver.FindElement(By.Id("NewPassword")).SendKeys("pass2pass1");
            driver.FindElement(By.Id("NewPassword")).Clear();
            driver.FindElement(By.Id("NewPassword")).SendKeys("pass2pass");
            driver.FindElement(By.Id("ConfirmPassword")).Clear();
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys("pass2pass");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Change Password\r(\n|\r\n)\r(\n|\r\n)Your password has been changed successfully\\. [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
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
