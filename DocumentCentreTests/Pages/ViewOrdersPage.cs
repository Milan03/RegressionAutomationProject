using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
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
            this.TypeDropDownLocator = HelperMethods.FindElement(Driver, "classname", "k-widget");
            this.OrderTable = new OrderTable(HelperMethods.FindElement(Driver, "id", "ordersGrid"));
            this.SearchOrdersButton = HelperMethods.FindElement(Driver, "id", "searchOrdersButton");

            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
        }

        /// <summary>
        /// For simulating order searchs by members
        /// </summary>
        /// <param name="type">Type of order to search for</param>
        /// <returns>Current page object</returns>
        public ViewOrdersPage SearchDraftOrders(string type)
        {
            TypeDropDownLocator.Click();
            switch (type)
            {
                case "Draft":
                    Driver.FindElement(By.XPath("id('orderStatus_listbox')/li[2]")).Click();
                    break;
                case "Pending":
                    Driver.FindElement(By.XPath("id('orderStatus_listbox')/li[3]")).Click();
                    break;
                case "Sent":
                    Driver.FindElement(By.XPath("id('orderStatus_listbox')/li[4]")).Click();
                    break;
                case "Processing":
                    Driver.FindElement(By.XPath("id('orderStatus_listbox')/li[5]")).Click();
                    break;
                case "Delivered":
                    Driver.FindElement(By.XPath("id('orderStatus_listbox')/li[6]")).Click();
                    break;
                default:
                    throw new Exception("Exception thrown in SearchDraftOrders");
            }
            SearchOrdersButton.Click();
            return this;
        }
    }
}
