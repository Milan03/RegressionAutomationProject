using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using NLog;

namespace DocumentCentreTests.Util
{
    /// <summary>contains functional methods frequently used during test</summary>
    internal static class HelperMethods
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
                    logger.Error("      Exception in HelperMethods.FindElement: No such element found.");
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

    }
}
