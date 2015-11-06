using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

using DocumentCentreTests.Util;
using OpenQA.Selenium.Support.PageObjects;

namespace DocumentCentreTests.Pages
{
    /// <summary>class representing Doc Centre Member Portal</summary>
    public class MemberHomePage :HomePage
    {
        private IWebDriver _driver;
        private IWebElement ordersDropdownLocator;

        /// <summary>
        /// Member home page constructor
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public MemberHomePage(IWebDriver driver)
        {
            this._driver = driver;
            PageFactory.InitElements(driver, this);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            IWebElement element =
            wait.Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.Id("userActionsButton")));
            // check if on correct page
            if (HelperMethods.FindElement(driver, "id", "userActionsButton") == null)
            {
                throw new NoSuchWindowException("Member homepage not found");
            }
            
            this.ordersDropdownLocator = HelperMethods.FindElement(driver, "linktext", "My Orders");
        }

        /// <summary>
        /// Logic to navigate to View Orders page
        /// </summary>
        /// <returns>New page object representing the destination.</returns>
        public override ViewOrdersPage NavigateToViewOrders()
        {
            // open dropdown
            ordersDropdownLocator.Click();
            var viewOrdersLink = HelperMethods.FindElement(_driver, "linktext", "View Orders");
            viewOrdersLink.Click();
            // check if on correct page
            if (!"My Orders".Equals(_driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
            return new ViewOrdersPage(_driver);
        }
    }
}
