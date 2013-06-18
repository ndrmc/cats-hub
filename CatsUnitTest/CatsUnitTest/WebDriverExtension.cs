using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CatsUnitTest
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebDriverExtension
    {
        public const int TIMEOUT_IN_SECONDS = 60;
        public const string BASE_URL ="http://localhost:37068";
        /// <summary>
        /// An extension method for an IWebDriver instance that searches for an element (given a regular expression string)
        /// </summary>
        /// <param name="regEx">The element to be searched for</param>
        public static void WaitForHttpResponse(this OpenQA.Selenium.IWebDriver driver, string regEx)
        {
            for (int second = 0; ; second++)
            {
                if (second >= TIMEOUT_IN_SECONDS)
                {
                    Assert.Fail("timeout");
                    //throw new TimeoutException("Response timed out");
                }
                try
                {
                    if (IsElementPresent(driver, By.XPath(regEx))) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }

        public static void WaitForHttpResponse(this OpenQA.Selenium.IWebDriver driver, By by)
        {
            for (int second = 0; ; second++)
            {
                if (second >= TIMEOUT_IN_SECONDS)
                {
                    Assert.Fail("timeout");
                    //throw new TimeoutException("Response timed out");
                }
                try
                {
                    if (IsElementPresent(driver, by)) break;
                }
                catch (Exception)
                { }
                Thread.Sleep(1000);
            }
        }

        public static void AutomateLogin(this OpenQA.Selenium.IWebDriver driver, string username, string password)
        {
            driver.WaitForHttpResponse(By.LinkText("Log On"));
            driver.FindElement(By.LinkText("Log On")).Click();
            driver.WaitForHttpResponse(By.Id("UserName"));
            driver.FindElement(By.Id("UserName")).Clear();
            driver.FindElement(By.Id("UserName")).SendKeys(username);
            driver.FindElement(By.Id("Password")).Clear();
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.CssSelector("input.t-button")).Click();
        }

        private static bool IsElementPresent(OpenQA.Selenium.IWebDriver driver, By by)
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

    }
}
