using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using NLog;

using DocumentCentreTests.Util;
using OpenQA.Selenium.Support.PageObjects;

namespace DocumentCentreTests.Pages
{
    public class LoginPage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;
        private IWebElement UsernameLocator;
        private IWebElement PasswordLocator;
        private IWebElement LoginButtonLocator;
        private string LoginPageType;

        /// <summary>
        /// Constructor assigns the Web Driver as well as all locators for elements needed
        /// in tests.
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public LoginPage(IWebDriver driver, string type)
        {
            this.Driver = driver;
            
            this.LoginPageType = type;
            this.UsernameLocator = HelperMethods.FindElement(driver, "name", "UserName");
            this.PasswordLocator = HelperMethods.FindElement(driver, "name", "Password");
            this.LoginButtonLocator = HelperMethods.FindElement(driver, "id", "loginButton");

            // check if on correct page
            if (!"User Login".Equals(driver.Title))
            {
                logger.Error("-- Member Login: FAILED");
                throw new NoSuchWindowException("Login page not found");
            }
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Login page allows user to type their username into username field
        /// </summary>
        /// <param name="username">string representing the username</param>
        /// <returns>Current page object</returns>
        public LoginPage TypeUsername(string username)
        {
            logger.Info("       - Inputing username");
            UsernameLocator.Clear();
            UsernameLocator.SendKeys(username);
            return this;
        }

        /// <summary>
        /// Login page allows user to type their password into the password field
        /// </summary>
        /// <param name="password">string representing the password</param>
        /// <returns>Current page object</returns>
        public LoginPage TypePassword(string password)
        {
            logger.Info("       - Inputing password");
            PasswordLocator.Clear();
            PasswordLocator.SendKeys(password);
            return this;
        }

        /// <summary>
        /// Login page allows the user to submit the login form
        /// </summary>
        /// <returns>New page object representing the destination.</returns>
        public HomePage SubmitLogin()
        {
            logger.Info("       - Submitting login");
            LoginButtonLocator.Click();
            if (LoginPageType.Equals("member"))
            {
                return new MemberHomePage(Driver);
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
            UsernameLocator.Clear();
            PasswordLocator.Clear();
            LoginButtonLocator.Click();
            return new LoginPage(Driver, LoginPageType);
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
            logger.Info("       - Attempting to login as: " +username);
            TypeUsername(username);
            TypePassword(password);
            return SubmitLogin();
        }
    }
}
