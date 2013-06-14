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
    public class InternalMovement
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
        public static void WaitForHttpRequest()
        {
            Thread.Sleep(3000);
        }
        
        [Test]
        public void TheInternalMovementTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin", "pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/InternalMovement");
            Assert.AreEqual("Commodity Tracking System : Internal Movment", driver.Title);
            Assert.AreEqual("", driver.FindElement(By.CssSelector("img")).Text);
            Assert.AreEqual("New internal movement", driver.FindElement(By.LinkText("New internal movement")).Text);
            driver.FindElement(By.LinkText("New internal movement")).Click();
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Record New Internal Transfer : DRMFSS - Diredawa Hub[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Project Code[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SI Number[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*From Store[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*From Stack[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Unit[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Quantity In Unit[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Quntity in Mt[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Date[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Ref Number[\\s\\S]*$"));
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Program[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.Id("CommodityId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("ProjectCodeId-input")));
            driver.WaitForHttpResponse(By.Id("ShippingInstructionId-input"));
            Assert.IsTrue(IsElementPresent(By.Id("ShippingInstructionId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("FromStoreId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("FromStackId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("UnitId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("ProgramId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("ToStoreId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("ProgramId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("ProgramId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("FromStackId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("UnitId-input")));
            //new Test
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div/ul/li[3]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table/tbody/tr[2]/td[2]/div/div/span/span")).Click();
            driver.WaitForHttpResponse(By.XPath("//div[5]/div/ul/li[2]"));
            driver.FindElement(By.XPath("//div[5]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table/tbody/tr[4]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[7]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("FromStackId-input")).Clear();
            driver.FindElement(By.Id("FromStackId-input")).SendKeys("2");
            driver.FindElement(By.XPath("//div[@id='body']/form/table/tbody/tr[6]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li[3]")).Click();
            driver.FindElement(By.Id("ReferenceNumber")).Clear();
            driver.FindElement(By.Id("ReferenceNumber")).SendKeys("123456");
            driver.FindElement(By.XPath("//div[@id='body']/form/table/tbody/tr[3]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[9]/div/ul/li[3]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table[2]/tbody/tr/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[10]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table[2]/tbody/tr/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[11]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table[3]/tbody/tr/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[12]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='body']/form/table[3]/tbody/tr/td[2]/div/div/span/span")).Click();
            //driver.WaitForHttpResponse("//div[12]/div/ul/li[2]");
            //driver.FindElement(By.XPath("//div[12]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("ApprovedBy")).Clear();
            driver.FindElement(By.Id("ApprovedBy")).SendKeys("Medhin Teklu");
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
            driver.WaitForHttpResponse(By.LinkText("Cancel"));
            driver.FindElement(By.LinkText("Cancel")).Click();
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
