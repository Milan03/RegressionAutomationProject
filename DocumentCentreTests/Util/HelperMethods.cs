using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests.Util
{
    /// <summary>contains functional methods frequently used during test</summary>
    internal static class HelperMethods
    {
        /// <summary>
        /// Wrapper for finding elements
        /// </summary>
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

    }
}
