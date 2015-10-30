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
    internal class HelperMethods
    {
        /// <summary>
        /// Helper method for finding elements
        /// </summary>
        public static IWebElement FindElement(IWebDriver driver, string type, string element)
        {
            if ("name".Equals(type))
            {
                var foundElement = driver.FindElement(By.Name(element));
                return foundElement;
            }
            else if ("id".Equals(type))
            {
                var foundElement = driver.FindElement(By.Id(element));
                return foundElement;
            }
            else if ("classname".Equals(type))
            {
                var foundElement = driver.FindElement(By.ClassName(element));
                return foundElement;
            }
            else if ("linktext".Equals(type))
            {
                var foundElement = driver.FindElement(By.LinkText(element));
                return foundElement;
            }
            else if ("tagname".Equals(type))
            {
                var foundElement = driver.FindElement(By.TagName(element));
                return foundElement;
            }
            else if ("css".Equals(type))
            {
                var foundElement = driver.FindElement(By.CssSelector(element));
                return foundElement;
            }
            else
                throw new NoSuchElementException("Element not found.");
        }
    }
}
