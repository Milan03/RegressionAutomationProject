using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

using DocumentCentreTests.Util;

namespace DocumentCentreTests.Pages
{
    /// <summary>class representing Doc Centre Member Portal</summary>
    public class MemberHomePage : HomePage
    {
        private IWebDriver driver;
        private IWebElement ordersDropdownLocator;

        /// <summary>
        /// Member home page constructor
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public MemberHomePage(IWebDriver driver)
        {
            // check if on correct page
            if (HelperMethods.FindElement(driver, "id", "userActionsButton") == null)
            {
                throw new NoSuchWindowException("Member homepage not found");
            }
            this.driver = driver;
            this.ordersDropdownLocator = HelperMethods.FindElement(driver, "linktext", "My Orders");
        }

        /// <summary>
        /// Logic to navigate to View Orders page
        /// </summary>
        /// <returns>New page object representing the destination.</returns>
        public override ViewOrdersPage NavigateToViewOrders()
        {
            ViewOrdersPage voPage = new ViewOrdersPage(driver);

            this.ordersDropdownLocator.Click();
            var viewOrdersLink = HelperMethods.FindElement(driver, "linktext", "View Orders");
            viewOrdersLink.Click();
            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
            return voPage;
        }
    }
}
