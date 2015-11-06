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
        private IWebElement SearchOrdersButton;
        private OrderTable OrderTable;

        /// <summary>
        /// Class representing View Orders for Members
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public ViewOrdersPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.TypeDropDownLocator = HelperMethods.FindElement(Driver, "classname", "k-dropdown");
            this.OrderTable = new OrderTable(HelperMethods.FindElement(Driver, "id", "ordersGrid"));
            this.SearchOrdersButton = HelperMethods.FindElement(Driver, "id", "searchOrdersButton");

            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
        }

        public void ChooseOrderType(string type)
        {
            var mySelect = new SelectElement(TypeDropDownLocator);
            var options = mySelect.Options;
            foreach (var option in options) {
                if (option.Text.Equals(type))
                    option.Click();
            }

        }

        public ViewOrdersPage SearchDraftOrders()
        {
            ChooseOrderType("Draft orders");
            SearchOrdersButton.Click();
            return this;
        }
    }
}
