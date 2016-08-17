using OpenQA.Selenium;
using System;
using NLog;
using System.Threading;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

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
                switch (type)
                {
                    case Constants.SearchType.NAME:
                        foundElement = driver.FindElement(By.Name(element));
                        break;
                    case Constants.SearchType.ID:
                        foundElement = driver.FindElement(By.Id(element));
                        break;
                    case Constants.SearchType.CLASSNAME:
                        foundElement = driver.FindElement(By.ClassName(element));
                        break;
                    case Constants.SearchType.LINKTEXT:
                        foundElement = driver.FindElement(By.LinkText(element));
                        break;
                    case Constants.SearchType.TAGNAME:
                        foundElement = driver.FindElement(By.TagName(element));
                        break;
                    case Constants.SearchType.CSS:
                        foundElement = driver.FindElement(By.CssSelector(element));
                        break;
                    case Constants.SearchType.XPATH:
                        foundElement = driver.FindElement(By.XPath(element));
                        break;
                    default:
                        _logger.Error(" > Unable to find element!");
                        foundElement = null;
                        break;
                }
                return foundElement;
            }
            catch (Exception e)
            {
                _logger.Error("-- TEST FAILURE @ ELEMENT NOT FOUND: " +element);
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                _logger.Fatal("-- TEST FAILURE @ EXCEPTION: " + e.Message);
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
        public static bool CheckAlert(IWebDriver driver)
        {
            try
            {
                Thread.Sleep(500);
                var alert = driver.FindElement(By.XPath(Constants.PO.XP.ALERT_SUCCESS));
                if (alert != null)
                    return true;
                else
                    return false;

            }
            catch (Exception e)
            {
                _logger.Fatal(" > Checking for alert [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
                throw new Exception(e.Message);
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

        /// <summary>
        /// Find a given string within a string.
        /// </summary>
        /// <param name="strSource">text source</param>
        /// <param name="strStart">first word</param>
        /// <param name="strEnd">second word</param>
        /// <returns></returns>
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
                return "";
        }

    }
}
