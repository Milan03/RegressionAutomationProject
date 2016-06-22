using OpenQA.Selenium;
using NLog;
using DocumentCentreTests.Util;

namespace DocumentCentreTests.Pages
{
    public class LoginPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        private IWebElement UsernameLocator;
        private IWebElement PasswordLocator;
        private IWebElement LoginButtonLocator;
        private string LoginPageType;

        internal bool LoginSuccess;
        /// <summary>
        /// Constructor assigns the Web Driver as well as all locators for elements needed
        /// in tests.
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public LoginPage(IWebDriver driver, string type)
        {
            _driver = driver;

            LoginPageType = type;
            LoginSuccess = false;
            UsernameLocator = HelperMethods.FindElement(driver, "name", "UserName");
            PasswordLocator = HelperMethods.FindElement(driver, "name", "Password");
            LoginButtonLocator = HelperMethods.FindElement(driver, "id", "loginButton");

            // check if on correct page
            if (!"User Login".Equals(driver.Title))
            {
                _logger.Fatal(" > Login page navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            } else
                _logger.Info(" > Login page reached!");

        }

        /// <summary>
        /// Login page allows user to type their username into username field
        /// </summary>
        /// <param name="username">string representing the username</param>
        /// <returns>Current page object</returns>
        public LoginPage TypeUsername(string username)
        {
            _logger.Info(" > Inputing username");
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
            _logger.Info(" > Inputing password");
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
            _logger.Info(" > Submitting login...");
            LoginButtonLocator.Click();
            switch (LoginPageType)
            {
                case "member":
                    LoginSuccess = true;
                    return new MemberHomePage(_driver);
                case "supplier":
                    LoginSuccess = true;
                    return new SupplierHomePage(_driver);
                default:
                    return null;
            }   
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
            return new LoginPage(_driver, LoginPageType);
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
            _logger.Info(" > Attempting to login as: " + username + "...");
            TypeUsername(username);
            TypePassword(password);
            return SubmitLogin();
        }
    }
}
