using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using NLog;

using DocumentCentreTests.Util;
using OpenQA.Selenium.Support.PageObjects;

namespace DocumentCentreTests.Pages
{
    /// <summary>class representing Doc Centre Member Portal</summary>
    public class MemberHomePage : HomePage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;
        private IWebElement OrdersDropdownLocator;

        /// <summary>
        /// Member home page constructor
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public MemberHomePage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(driver, this);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            IWebElement element =
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("userActionsButton")));
            // check if on correct page
            if (!HelperMethods.FindElement(driver, "id", "userActionsButton").Displayed)
            {
                throw new NoSuchWindowException("Member homepage not found");
            }

            logger.Info("Login successful.");
            this.OrdersDropdownLocator = HelperMethods.FindElement(driver, "linktext", "My Orders");
        }

        /// <summary>
        /// Logic to navigate to View Orders page
        /// </summary>
        /// <returns>New page object representing the destination.</returns>
        public override ViewOrdersPage NavigateToViewOrders()
        {
            logger.Info("Attempting to navigate to View Orders...");
            // open dropdown
            OrdersDropdownLocator.Click();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));
            var viewOrdersLink = HelperMethods.FindElement(Driver, "linktext", "View Orders");
            viewOrdersLink.Click();
            // check if on correct page
            if (!"My Orders".Equals(Driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
            logger.Info("View Orders page reached.");
            return new ViewOrdersPage(Driver);
        }
    }
}
