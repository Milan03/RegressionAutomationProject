using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests
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
            else
                throw new InvalidElementStateException("Element not found.");
        }

        /// <summary>
        /// Method used to login to http://portal.test-web01.lbmx.com/login?redirect=%2f
        /// </summary>
        //public static void Login(IWebDriver driver, string username, string password)
        //{
        //    // find login box
        //    var usernameTextbox = driver.FindElement(By.Name("UserName"));
        //    usernameTextbox.Clear();
        //    usernameTextbox.SendKeys(username);
        //    // find password box
        //    var passwordTextbox = driver.FindElement(By.Name("Password"));
        //    passwordTextbox.Clear();
        //    passwordTextbox.SendKeys(password);
        //    // find Log In button
        //    var loginButton = driver.FindElement(By.Id("loginButton"));
        //    loginButton.Click();
            
        //    // verify the browser was navigated to the correct page
        //    WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(20));
        //    IWebElement messageElement = wait.Until(ExpectedConditions.ElementExists(By.Id("userActionsButton")));

        //    //if (wait.)
        //    //{

        //    //}

        //    //Assert.FullMatch(driver.Url, "http://portal.test-web01.lbmx.com/");
        //}
    }
}
