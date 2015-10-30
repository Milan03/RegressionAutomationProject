using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

using DocumentCentreTests.Tables;
using DocumentCentreTests.Util;

namespace DocumentCentreTests.Pages
{
    public class ViewOrdersPage
    {
        private IWebDriver Driver;
        private IWebElement TypeDropDownLocator;
        private OrderTable OrderTable;

        /// <summary>
        /// Class representing View Orders for Members
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public ViewOrdersPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.TypeDropDownLocator = HelperMethods.FindElement(Driver, "class", "k-dropdown");
            this.OrderTable = new OrderTable(HelperMethods.FindElement(Driver, "css", "table"));

            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
        }

        public ViewOrdersPage SearchDraftOrders()
        {

            return this;
        }
    }
}
