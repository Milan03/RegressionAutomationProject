using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        private IWebElement usernameLocator;
        private IWebElement passwordLocator;
        private IWebElement loginButtonLocator;
        private string loginPageType;

        /// <summary>
        /// Constructor assigns the Web Driver as well as all locators for elements needed
        /// in tests.
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public LoginPage(IWebDriver driver, string type)
        {
            this.driver = driver;
            this.loginPageType = type;
            this.usernameLocator = HelperMethods.FindElement(driver, "name", "UserName");
            this.passwordLocator = HelperMethods.FindElement(driver, "name", "Password");
            this.loginButtonLocator = HelperMethods.FindElement(driver, "id", "loginButton");

            // check if on correct page
            if (!"User Login".Equals(driver.Title))
            {
                // TODO: logic to navigate back to login page
                throw new NoSuchWindowException("Login page not found");
            }
        }

        /// <summary>
        /// Login page allows user to type their username into username field
        /// </summary>
        /// <param name="username">string representing the username</param>
        /// <returns>Current page object</returns>
        public LoginPage TypeUsername(string username)
        {
            usernameLocator.Clear();
            usernameLocator.SendKeys(username);
            return this;
        }

        /// <summary>
        /// Login page allows user to type their password into the password field
        /// </summary>
        /// <param name="password">string representing the password</param>
        /// <returns>Current page object</returns>
        public LoginPage TypePassword(string password)
        {
            passwordLocator.Clear();
            passwordLocator.SendKeys(password);
            return this;
        }

        /// <summary>
        /// Login page allows the user to submit the login form
        /// </summary>
        /// <returns>New page object representing the destination.</returns>
        public HomePage SubmitLogin()
        {
            
            loginButtonLocator.Click();
            if (loginPageType.Equals("member"))
            {
                HomePage newHomePage = new MemberHomePage(driver);
                return newHomePage;
            }
            else
                return null;
                
        }

        /// <summary>
        /// Login page allows user to submit form knowing that an invalid username
        /// and/or password will be entered
        /// </summary>
        /// <returns>New page object representing the destination page.</returns>
        public LoginPage SubmitLoginExpectingFailure()
        {
            loginButtonLocator.Click();
            LoginPage newLogin = new LoginPage(driver, loginPageType);
            return newLogin;
        }

        /// <summary>
        /// Login page offers user the service to "log into" application using
        /// a username and password
        /// </summary>
        /// <param name="username">string representing the username</param>
        /// <param name="password">string representing the password</param>
        /// <returns>New page object representing the destination page.</returns>
        public HomePage LoginAs(string username, string password)
        {
            TypeUsername(username);
            TypePassword(password);
            return SubmitLogin();
        }
    }
}
