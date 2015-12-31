﻿using OpenQA.Selenium;
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
        /// <returns>Element object on page if found</returns>
        public static IWebElement FindElement(IWebDriver driver, string type, string element)
        {
            IWebElement foundElement;
            try
            {
                if (type.Equals("name"))
                    foundElement = driver.FindElement(By.Name(element));
                else if (type.Equals("id"))
                    foundElement = driver.FindElement(By.Id(element));
                else if (type.Equals("classname"))
                    foundElement = driver.FindElement(By.ClassName(element));
                else if (type.Equals("linktext"))
                    foundElement = driver.FindElement(By.LinkText(element));
                else if (type.Equals("tagname"))
                    foundElement = driver.FindElement(By.TagName(element));
                else if (type.Equals("css"))
                    foundElement = driver.FindElement(By.CssSelector(element));
                else if (type.Equals("xpath"))
                    foundElement = driver.FindElement(By.XPath(element));
                else
                {
                    _logger.Error("    - ERROR: Element [type: " + type + ", name: " + element + "] could not be located.");
                    foundElement = null;
                }
                return foundElement;
            }
            catch (NoSuchElementException)
            {
                _logger.Error("    - ERROR: Element [type: " + type + ", name: " + element + "] could not be located.");
                _logger.Fatal("-- TEST FAILURE @ URL: '" +driver.Url +"' --");
                BaseDriverTest.TakeScreenshot("screenshot");
                throw new NoSuchElementException("Unable to locate element.");
            }
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

        /// <summary>
        /// Gets the message of an alert popup
        /// </summary>
        /// <param name="driver">Testing interface</param>
        /// <returns>The alert message as string</returns>
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

        /// <summary>
        /// Checks alert message when an item is added for accurate result of addition
        /// </summary>
        /// <param name="driver">Testing interface</param>
        /// <param name="prod">Product added to cart</param>
        /// <returns>Boolean representing a pass or fail</returns>
        public static bool CheckItemAddAlert(IWebDriver driver, Product prod)
        {
            try
            {
                Thread.Sleep(2000);
                var shouldBe = "Your cart has been updated and now contains "
                    + prod.Quantity.ToString() + " units of '" + prod.ProductTitle.Text +
                    "' for the total amount of $" + prod.getAmountTotal().ToString() + ".";
                var alertMsg = driver.FindElement(By.XPath(Constants.XPATH_ALERT_MSG)).Text;
                if (alertMsg.Equals(shouldBe))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                _logger.Fatal("         - Checking for alert [FAILED]");
                throw new Exception("Exception thrown in CheckItemAddAlert()");
            }
        }

        public static bool CheckItemDeleteAlert(IWebDriver driver, CartItem item)
        {
            try
            {
                var shouldBe = "×\r\n'" + item.Description.Text + "' has been removed from the cart.";
                var alertMsg = driver.FindElement(By.XPath(Constants.XPATH_ALERT_DEL)).Text;
                if (alertMsg.Equals(shouldBe))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                _logger.Fatal("         - Checking for delete alert [FAILED]");
                throw new Exception("Exception thrown in CheckItemDeleteAlert()");
            }
        }

        /// <summary>
        /// Generates a random string
        /// </summary>
        /// <param name="length">length of string</param>
        /// <returns>new random string</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
