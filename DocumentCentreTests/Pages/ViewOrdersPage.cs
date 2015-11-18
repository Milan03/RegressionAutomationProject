﻿using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using DocumentCentreTests.Tables;
using DocumentCentreTests.Util;
using NLog;

namespace DocumentCentreTests.Pages
{
    public class ViewOrdersPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;
        private IWebElement TypeDropDownLocator;
        private IWebElement POInputLocator;
        private IWebElement SearchOrdersButton;
        public IWebElement FirstTableElem { get; set;  }

        /// <summary>
        /// Class representing View Orders for Members
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public ViewOrdersPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.TypeDropDownLocator = HelperMethods.FindElement(Driver, "classname", "k-widget");
            this.POInputLocator = HelperMethods.FindElement(Driver, "id", "poNumber");
            this.SearchOrdersButton = HelperMethods.FindElement(Driver, "id", "searchOrdersButton");

            // get first table element
            CheckFirstRow();

            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                throw new NoSuchWindowException("       - View Orders page not found");
            }
        }

        /// <summary>
        /// Check the first row of the table for value or if no elements present
        /// set to appropriate message
        /// </summary>
        public void CheckFirstRow()
        {
            if (HelperMethods.IsElementPresent(Driver, By.XPath(Constants.XPATH_TABLE_PO)))
                this.FirstTableElem = Driver.FindElement(By.XPath(Constants.XPATH_TABLE_PO));
            else // no table elements found, set to message
                this.FirstTableElem = Driver.FindElement(By.XPath("id('ordersGrid')/div[2]/div[2]"));
                
        }

        /// <summary>
        /// For simulating order searchs by members
        /// </summary>
        /// <param name="type">Type of order to search for</param>
        /// <returns>Current page object</returns>
        public ViewOrdersPage ChooseOrderType(string type)
        {
            _logger.Info("       - Choosing order type: " +type);
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
            return this;
        }

        /// <summary>
        /// Simulates clicking the Search button
        /// </summary>
        /// <returns>Current page object</returns>
        public ViewOrdersPage Search()
        {
            _logger.Info("       - Searching for order");
            SearchOrdersButton.Click();
            return this;
        }

        /// <summary>
        /// Enter specific PO to search for
        /// </summary>
        /// <param name="po">PO to search for</param>
        /// <returns>Current page object</returns>
        public ViewOrdersPage InputPurchaseOrder(string po)
        {
            _logger.Info("       - Inputting purchase order number: " +po);
            POInputLocator.Click();
            POInputLocator.Clear();
            POInputLocator.SendKeys(po);
            return this;
        }
    }
}
