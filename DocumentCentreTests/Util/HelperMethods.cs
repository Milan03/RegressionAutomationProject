using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using NLog;
using System.Threading;
using System.Linq;
using DocumentCentreTests.Catalogue;

namespace DocumentCentreTests.Util
{
    /// <summary>contains functional methods frequently used during test</summary>
    internal static class HelperMethods
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        /// <summary>Wrapper for finding elements
        /// <param name="driver">Interface for testing</param>
        /// <param name="type">Type to search by</param>
        /// <param name="element">Value to search for</param>
        /// <returns></returns>
        public static IWebElement FindElement(IWebDriver driver, string type, string element)
        {
            IWebElement foundElement;
            switch (type)
            {
                case "name":
                    foundElement = driver.FindElement(By.Name(element));
                    break;
                case "id":
                    foundElement = driver.FindElement(By.Id(element));
                    break;
                case "classname":
                    foundElement = driver.FindElement(By.ClassName(element));
                    break;
                case "linktext":
                    foundElement = driver.FindElement(By.LinkText(element));
                    break;
                case "tagname":
                    foundElement = driver.FindElement(By.TagName(element));
                    break;
                case "css":
                    foundElement = driver.FindElement(By.CssSelector(element));
                    break;
                case "xpath":
                    foundElement = driver.FindElement(By.XPath(element));
                    break;
                default:
                    _logger.Error("      Exception in HelperMethods.FindElement: No such element found.");
                    throw new NoSuchElementException("Element not found.");

            }
            return foundElement;
        }

        /// <summary>
        /// Wrapper for Selenium JavaScript executor
        /// </summary>
        /// <param name="driver">Main interface for testing</param>
        /// <returns>Interface through which the user can execute JavaScript</returns>
        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        /// <summary>
        /// Function to determine if an element is present on current page object
        /// </summary>
        /// <param name="driver">Testing interface driver</param>
        /// <param name="by">By parameter to search with</param>
        /// <returns>Boolean represeting a pass or fail</returns>
        public static bool IsElementPresent(IWebDriver driver, By by)
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

        public static string CheckAlert(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(2000);
                var alertMsg = driver.FindElement(By.XPath(Constants.XPATH_ALERT_MSG)).Text;
                return alertMsg;
                
            }
            catch (Exception)
            {
                _logger.Fatal("         - Checking for alert [FAILED]");
                throw new Exception("Exception thrown in CheckAlert()");
            }
        }

        public static bool CheckItemAlert(IWebDriver driver, Product prod)
        {
            try
            {
                Thread.Sleep(2000);
                var shouldBe = "Your cart has been updated and now contains " 
                    + prod.Quantity.ToString() + " units of '" + prod.Description + 
                    "' for the total amount of $" + prod.AmountTotal.ToString() + ".";
                var alertMsg = driver.FindElement(By.XPath(Constants.XPATH_ALERT_MSG)).Text;
                if (alertMsg.Equals(shouldBe))
                    return true;
                else 
                    return false;
            }
            catch (Exception)
            {
                _logger.Fatal("         - Checking for alert [FAILED]");
                throw new Exception("Exception thrown in CheckAlert()");
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
