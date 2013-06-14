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
    public class Receipts
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
        public void TheReceiptsTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.AutomateLogin("admin","pass2pass");
            driver.Navigate().GoToUrl(baseURL + "/Receive");
            Assert.AreEqual("Commodity Tracking System : Expected Receipts", driver.Title);
            Assert.AreEqual("", driver.FindElement(By.CssSelector("img")).Text);
            Assert.AreEqual("From Donation", driver.FindElement(By.CssSelector("div.field-set-title > h2")).Text);
            Assert.AreEqual("Make a new Plan", driver.FindElement(By.LinkText("Make a new Plan")).Text);
            Assert.IsTrue(IsElementPresent(By.LinkText("Record a new receipt")));
            
            //Assert.AreEqual("off", driver.FindElement(By.Id("chk-closedtoo-1")).GetAttribute("value"));
            Assert.AreEqual("Food", driver.FindElement(By.Id("CommodityTypeSelector-1-input")).GetAttribute("value"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*From Loan, Swap, Repayment Or Transfer[\\s\\S]*$"));
            Assert.AreEqual("Make a new Plan", driver.FindElement(By.XPath("(//a[contains(text(),'Make a new Plan')])[2]")).Text);
            Assert.AreEqual("From Loan", driver.FindElement(By.LinkText("From Loan")).Text);
            Assert.AreEqual("From Repayment", driver.FindElement(By.LinkText("From Repayment")).Text);
            Assert.AreEqual("From Swap", driver.FindElement(By.LinkText("From Swap")).Text);
            Assert.AreEqual("From Transfer", driver.FindElement(By.LinkText("From Transfer")).Text);
            try
            {
                Assert.AreEqual("off", driver.FindElement(By.Id("chk-closedtoo-2")).GetAttribute("value"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.AreEqual("Food", driver.FindElement(By.Id("CommodityTypeSelector-2-input")).GetAttribute("value"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Local purchase[\\s\\S]*$"));
            Assert.AreEqual("Make a new Plan", driver.FindElement(By.XPath("(//a[contains(text(),'Make a new Plan')])[3]")).Text);
            driver.FindElement(By.LinkText("Make a new Plan")).Click();
            // Warning: assertTextPresent may require manual changes
            driver.WaitForHttpResponse(By.Id("CommoditySourceID-input"));
            Assert.IsTrue(IsElementPresent(By.Id("CommoditySourceID-input")));
            Assert.IsTrue(IsElementPresent(By.Id("DonorID-input")));
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[9]/td[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("CommodityTypeID-input")));
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[9]/td[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("ProgramID-input")));
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[9]/td[2]")).Click();
            Assert.IsTrue(IsElementPresent(By.Id("QuantityInUnit")));
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("789654");
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[5]/td[2]/div/div/span/span")).Click();
            
            //driver.WaitForHttpResponse(By.XPath("//div[11]/div/ul/li[7]"));
            //driver.FindElement(By.XPath("//div[11]/div/ul/li[7]")).Click();
            //driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[2]/div/div/span/span")).Click();
            //driver.WaitForHttpResponse(By.XPath("//div[12]/div/ul/li[2]"));
            //driver.FindElement(By.XPath("//div[12]/div/ul/li[2]")).Click();
            //driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[8]/td[2]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//div[13]/div/ul/li[2]")).Click();
            //driver.FindElement(By.Id("QuantityInUnit")).Clear();
            //driver.FindElement(By.Id("QuantityInUnit")).SendKeys("124");
            //driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[4]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//div[14]/div/ul/li[3]")).Click();
            //driver.FindElement(By.CssSelector("input[type=\"button\"]")).Click();
            //driver.FindElement(By.CssSelector("input.t-button")).Click();
            //driver.FindElement(By.Id("SINumber")).Clear();
            //driver.FindElement(By.Id("SINumber")).SendKeys("123465678");
            //driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            //driver.FindElement(By.CssSelector("body")).Click();
            //driver.FindElement(By.LinkText("Edit")).Click();
            //driver.FindElement(By.CssSelector("input.t-button")).Click();
            //driver.FindElement(By.LinkText("Receive")).Click();
            //driver.FindElement(By.Id("GRN")).Clear();
            //driver.FindElement(By.Id("GRN")).SendKeys("852369741");
            //driver.FindElement(By.XPath("//div[@id='receive_form']/div[3]/table/tbody/tr[7]/td[2]/div/div/span/span")).Click();
            //driver.FindElement(By.XPath("//div/ul/li[2]")).Click();
            //driver.FindElement(By.Id("WayBillNo")).Clear();
            //driver.FindElement(By.Id("WayBillNo")).SendKeys("1236544");
            //driver.FindElement(By.Id("GRN")).Clear();
            //driver.FindElement(By.Id("GRN")).SendKeys("85236974");
            //driver.FindElement(By.Id("GRN")).Clear();
            //driver.FindElement(By.Id("GRN")).SendKeys("8523697");
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*New / Edit Receipt : DRMFSS - Diredawa Hub[\\s\\S]*$"));
            Assert.AreEqual("", driver.FindElement(By.CssSelector("img")).Text);
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*GRN Details[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*GRN[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity Source[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SI / Batch Number[\\s\\S]*$"));
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Program[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Commodity Type[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Store[\\s\\S]*$"));
            Assert.AreEqual("Commodity", driver.FindElement(By.CssSelector("td")).Text);
            Assert.IsTrue(IsElementPresent(By.XPath("//table[@id='commodity0']/tbody/tr[2]/td")));
            Assert.IsTrue(IsElementPresent(By.XPath("//table[@id='commodity0']/tbody/tr[3]/td")));
            Assert.IsTrue(IsElementPresent(By.XPath("//table[@id='commodity0']/tbody/tr[4]/td")));
            Assert.IsTrue(IsElementPresent(By.LinkText("Add new record")));
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.LinkText("Add new record")).Click();
            driver.FindElement(By.XPath("//div[5]/div/ul/li")).Click();
            driver.FindElement(By.LinkText("Delete")).Click();
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Are you sure you want to delete this record[\\s\\S]$"));
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.Id("UnitID-input")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Transportation Details[\\s\\S]*$"));
            // Warning: assertTextPresent may require manual changes
            Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Responsible Donor \\(Implementer\\)[\\s\\S]*$"));
            Assert.IsTrue(IsElementPresent(By.Id("ResponsibleDonorID-input")));
            driver.FindElement(By.XPath("//div[7]/div/ul/li")).Click();
            Assert.AreEqual("", driver.FindElement(By.Id("TicketNumber")).GetAttribute("value"));
            driver.FindElement(By.Id("TicketNumber")).Clear();
            driver.FindElement(By.Id("TicketNumber")).SendKeys("456987");
            Assert.AreEqual("", driver.FindElement(By.Id("WeightBeforeUnloading")).Text);
            driver.FindElement(By.Id("WeightBeforeUnloading")).Clear();
            driver.FindElement(By.Id("WeightBeforeUnloading")).SendKeys("150");
            Assert.AreEqual("", driver.FindElement(By.Id("TransporterID-input")).GetAttribute("value"));
            driver.FindElement(By.XPath("//div[8]/div/ul/li[3]")).Click();
            Assert.AreEqual("", driver.FindElement(By.Id("PlateNo_Prime")).GetAttribute("value"));
            Assert.AreEqual("", driver.FindElement(By.Id("PortName")).GetAttribute("value"));
            driver.FindElement(By.Id("PlateNo_Prime")).Clear();
            driver.FindElement(By.Id("PlateNo_Prime")).SendKeys("AA36985");
            driver.FindElement(By.Id("PortName")).Clear();
            driver.FindElement(By.Id("PortName")).SendKeys("Dijibuti");
            Assert.AreEqual("", driver.FindElement(By.Id("SourceDonorID-input")).GetAttribute("value"));
            driver.FindElement(By.XPath("//div[9]/div/ul/li[6]")).Click();
            driver.FindElement(By.Id("WeightAfterUnloading")).Clear();
            driver.FindElement(By.Id("WeightAfterUnloading")).SendKeys("140");
            driver.FindElement(By.Id("englishplain-DriverName")).Click();
            Assert.AreEqual("", driver.FindElement(By.Id("DriverName")).GetAttribute("value"));
            driver.FindElement(By.Id("DriverName")).Clear();
            driver.FindElement(By.Id("DriverName")).SendKeys("Abebe Alemyahu");
            driver.FindElement(By.Id("PlateNo_Trailer")).Clear();
            driver.FindElement(By.Id("PlateNo_Trailer")).SendKeys("123456");
            driver.FindElement(By.Id("VesselName")).Clear();
            driver.FindElement(By.Id("VesselName")).SendKeys("ET789");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            Assert.AreEqual("Record a new receipt", driver.FindElement(By.XPath("(//a[contains(text(),'Record a new receipt')])[2]")).Text);
            // ERROR: Caught exception [unknown command []]
            driver.FindElement(By.LinkText("Make a new Plan")).Click();
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("00020272");
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[5]/td[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[3]/td[2]/div/div/span")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[5]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//li[15]")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[10]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[8]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[11]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("Remark")).Clear();
            driver.FindElement(By.Id("Remark")).SendKeys("some remarks here");
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[12]/div/ul/li[3]")).Click();
            driver.FindElement(By.Id("QuantityInMT")).Clear();
            driver.FindElement(By.Id("QuantityInMT")).SendKeys("45");
            driver.FindElement(By.CssSelector("input[type=\"button\"]")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.LinkText("Edit")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[14]/div/ul/li[5]")).Click();
            driver.FindElement(By.Id("QuantityInMT")).Clear();
            driver.FindElement(By.Id("QuantityInMT")).SendKeys("450");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Make a new Plan')])[2]")).Click();
            driver.FindElement(By.CssSelector("span.t-icon.t-close")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Make a new Plan')])[2]")).Click();
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("123654");
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[17]/div/ul/li[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[5]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[18]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[7]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[19]/div/ul/li")).Click();
            driver.FindElement(By.Id("OtherDocumentationRef")).Clear();
            driver.FindElement(By.Id("OtherDocumentationRef")).SendKeys("852369");
            driver.FindElement(By.Id("CommoditySourceID-input")).Click();
            driver.FindElement(By.XPath("//div[20]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("CommodityID-input")).Click();
            driver.FindElement(By.XPath("//div[21]/div/ul/li[5]")).Click();
            driver.FindElement(By.Id("QuantityInMT")).Clear();
            driver.FindElement(By.Id("QuantityInMT")).SendKeys("100");
            driver.FindElement(By.Id("Remark")).Clear();
            driver.FindElement(By.Id("Remark")).SendKeys("some remarks here");
            driver.FindElement(By.CssSelector("input[type=\"button\"]")).Click();
            driver.FindElement(By.CssSelector("input[type=\"button\"]")).Click();
            driver.FindElement(By.Id("ProgramID-input")).Click();
            driver.FindElement(By.CssSelector("#ReceiptAllocationForm > p")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[8]")).Click();
            driver.FindElement(By.Id("ProjectNumber")).Clear();
            driver.FindElement(By.Id("ProjectNumber")).SendKeys("100/200");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.Id("chk-closedtoo-2")).Click();
            driver.FindElement(By.Id("chk-closedtoo-2")).Click();
            driver.FindElement(By.Id("chk-closedtoo-1")).Click();
            driver.FindElement(By.Id("chk-closedtoo-1")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Make a new Plan')])[3]")).Click();
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*SI / Batch Number [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*Purchase Order [\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("123456");
            // Warning: verifyTextPresent may require manual changes
            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.CssSelector("BODY")).Text, "^[\\s\\S]*The SInumber given is Not valid for this Commodity Source[\\s\\S]*$"));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("963258");
            driver.FindElement(By.Id("PurchaseOrder")).Clear();
            driver.FindElement(By.Id("PurchaseOrder")).SendKeys("123456");
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[3]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[24]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[5]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[25]/div/ul/li[16]")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[26]/div/ul/li")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[8]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[27]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("SupplierName")).Clear();
            driver.FindElement(By.Id("SupplierName")).SendKeys("Supplier Name");
            driver.FindElement(By.Id("Remark")).Clear();
            driver.FindElement(By.Id("Remark")).SendKeys("some remarks here");
            driver.FindElement(By.Id("OtherDocumentationRef")).Clear();
            driver.FindElement(By.Id("OtherDocumentationRef")).SendKeys("123456");
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[28]/div/ul/li[5]")).Click();
            driver.FindElement(By.Id("QuantityInMT")).Clear();
            driver.FindElement(By.Id("QuantityInMT")).SendKeys("100");
            driver.FindElement(By.CssSelector("input[type=\"button\"]")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            driver.FindElement(By.Id("SINumber")).Clear();
            driver.FindElement(By.Id("SINumber")).SendKeys("789654");
            driver.FindElement(By.CssSelector("input.t-button.valid")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Edit')])[9]")).Click();
            driver.FindElement(By.XPath("//div[@id='ReceiptAllocationForm']/table/tbody/tr[6]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[30]/div/ul/li[5]")).Click();
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.XPath("(//a[contains(text(),'Receive')])[9]")).Click();
            driver.FindElement(By.Id("GRN")).Clear();
            driver.FindElement(By.Id("GRN")).SendKeys("123456");
            driver.FindElement(By.XPath("//div[@id='receive_form']/div[3]/table/tbody/tr[7]/td[2]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div/ul/li[2]")).Click();
            driver.FindElement(By.Id("WayBillNo")).Clear();
            driver.FindElement(By.Id("WayBillNo")).SendKeys("123456");
            driver.FindElement(By.XPath("//div[@id='receive_form']/div[3]/table/tbody/tr[7]/td[4]/div/div/span/span")).Click();
            driver.FindElement(By.XPath("//div[5]/div/ul/li[4]")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.XPath("//div[6]/div/ul/li")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.XPath("//div[7]/div/ul/li[2]")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.CssSelector("td.t-state-focused")).Click();
            driver.FindElement(By.Id("TicketNumber")).Clear();
            driver.FindElement(By.Id("TicketNumber")).SendKeys("74125");
            driver.FindElement(By.Id("WeightBeforeUnloading")).Clear();
            driver.FindElement(By.Id("WeightBeforeUnloading")).SendKeys("120");
            driver.FindElement(By.Id("TransporterID-input")).Click();
            driver.FindElement(By.XPath("//div[8]/div/ul/li[3]")).Click();
            driver.FindElement(By.Id("PlateNo_Prime")).Clear();
            driver.FindElement(By.Id("PlateNo_Prime")).SendKeys("AA36985");
            driver.FindElement(By.Id("PortName")).Clear();
            driver.FindElement(By.Id("PortName")).SendKeys("Dijibuti");
            driver.FindElement(By.Id("SourceDonorID-input")).Click();
            driver.FindElement(By.XPath("//div[9]/div/ul/li[2]")).Click();
            driver.FindElement(By.Id("WeightAfterUnloading")).Clear();
            driver.FindElement(By.Id("WeightAfterUnloading")).SendKeys("100");
            driver.FindElement(By.Id("englishplain-DriverName")).Click();
            driver.FindElement(By.Id("DriverName")).Clear();
            driver.FindElement(By.Id("DriverName")).SendKeys("Abebe Alemyahu");
            driver.FindElement(By.Id("PlateNo_Trailer")).Clear();
            driver.FindElement(By.Id("PlateNo_Trailer")).SendKeys("123456");
            driver.FindElement(By.Id("VesselName")).Clear();
            driver.FindElement(By.Id("VesselName")).SendKeys("ET789");
            driver.FindElement(By.Id("englishplain-Remark")).Click();
            driver.FindElement(By.Id("Remark")).Clear();
            driver.FindElement(By.Id("Remark")).SendKeys("Enter some remarks here");
            driver.FindElement(By.CssSelector("input.t-button")).Click();
            driver.FindElement(By.Id(":2m")).Click();
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
