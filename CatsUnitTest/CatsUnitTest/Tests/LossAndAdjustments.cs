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
    public class LossAndAdjustments
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
           // driver.Manage().Window.Size(new Point(-2000, 0));
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
        public void TheLossAndAdjustmentsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/LossesAndAdjustments");
          //  driver.FindElement(By.LinkText("Losses and Adjustments")).Click();
            Assert.AreEqual("Commodity Tracking System : Loss or Adjustment", driver.Title);
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Loss or Adjustment : DRMFSS - Diredawa Hub[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.IsTrue(IsElementPresent(By.Id("LossAdjustmentFilter-input")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Record new Loss")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Record new Adjustment")));
            driver.FindElement(By.LinkText("Record new Loss")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Record new Loss : DRMFSS - Diredawa Hub[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Project Code[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SI Number[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store[\\s\\S]*$"));
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div/ul/li[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("CommodityId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[2]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[5]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ProjectCodeId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ShippingInstructionId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[4]/td[2]/div/div/span/span")).Click();
            driver.WaitForHttpResponse(By.XPath("//div[7]/div/ul/li[2]"));
            driver.FindElement(By.XPath("//div[7]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("StoreId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("StoreMan")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[6]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ReasonId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("UnitId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[7]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[9]/div/ul/li[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("QuantityInUint")));
            driver.FindElement(By.Id("QuantityInUint")).Clear();
            driver.FindElement(By.Id("QuantityInUint")).SendKeys("19");
            driver.FindElement(By.Id("QuantityInMt")).Clear();
            driver.FindElement(By.Id("QuantityInMt")).SendKeys("10");
            Assert.IsTrue(IsElementPresent(By.Id("ApprovedBy")));
            driver.FindElement(By.Id("ApprovedBy")).Clear();
            driver.FindElement(By.Id("ApprovedBy")).SendKeys("Medhin Teklu");
            Assert.IsTrue(IsElementPresent(By.Id("MemoNumber")));
            driver.FindElement(By.Id("MemoNumber")).Clear();
            driver.FindElement(By.Id("MemoNumber")).SendKeys("1223456");
            Assert.IsTrue(IsElementPresent(By.Id("ProgramId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[3]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[10]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("input.t-button")));
            Assert.IsTrue(IsElementPresent(By.XPath("//input[@value='Cancel']")));
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            Assert.IsTrue(IsElementPresent(By.CssSelector("input[type=\"button\"]")));
            driver.Navigate().GoToUrl(baseURL + "/LossesAndAdjustments/CreateAdjustment");
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Record new Adjustment : DRMFSS - Diredawa Hub[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Project Code[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SI Number[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store Man[\\s\\S]*$"));
            driver.FindElement(By.CssSelector("span.t-select.t-header > span.t-icon.t-arrow-down")).Click();
            driver.FindElement(By.XPath("//div/ul/li[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("CommodityId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[2]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[5]/div/ul/li[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ProjectCodeId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ShippingInstructionId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[4]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[7]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("StoreId-input")));
            Assert.IsTrue(IsElementPresent(By.Id("StoreMan")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[6]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ReasonId-input")));
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[7]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[9]/div/ul/li[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("UnitId-input")));
            driver.FindElement(By.Id("QuantityInUint")).Clear();
            driver.FindElement(By.Id("QuantityInUint")).SendKeys("10");
            driver.FindElement(By.Id("QuantityInMt")).Clear();
            driver.FindElement(By.Id("QuantityInMt")).SendKeys("2");
            driver.FindElement(By.Id("ApprovedBy")).Clear();
            driver.FindElement(By.Id("ApprovedBy")).SendKeys("Medhin Teklu");
            Assert.IsTrue(IsElementPresent(By.Id("SelectedDate")));
            Assert.IsTrue(IsElementPresent(By.Id("MemoNumber")));
            driver.FindElement(By.Id("MemoNumber")).Clear();
            driver.FindElement(By.Id("MemoNumber")).SendKeys("123456");
            driver.FindElement(By.XPath("//div[@id='body']/div[2]/form/table/tbody/tr[3]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[10]/div/ul/li[3]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ProgramId-input")));
            Assert.IsTrue(IsElementPresent(By.CssSelector("input.t-button")));
            Assert.IsTrue(IsElementPresent(By.XPath("//input[@value='Cancel']")));
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
