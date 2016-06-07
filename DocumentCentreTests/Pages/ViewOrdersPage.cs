using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using DocumentCentreTests.Util;
using NLog;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class ViewOrdersPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;
        private IWebElement OrderTypeDropdown;
        private IWebElement POInputTextbox;
        private IWebElement SearchOrdersButton;
        private IWebElement DeleteOrderLocator;

        internal IWebElement FirstTableElem { get; set;  }
        internal bool AlertSuccess { get; set; }
        internal string OrderType { get; set; }

        /// <summary>
        /// Class representing View Orders for Members
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public ViewOrdersPage(IWebDriver driver)
        {
            _logger.Info(" > View Orders Page is being constructed...");
            this.Driver = driver;
            this.OrderTypeDropdown = HelperMethods.FindElement(Driver, "classname", "k-widget");
            this.POInputTextbox = HelperMethods.FindElement(Driver, "id", "poNumber");
            this.SearchOrdersButton = HelperMethods.FindElement(Driver, "id", "searchOrdersButton");
            this.OrderType = "All";

            // get first table element
            CheckFirstRow();

            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                _logger.Fatal(" > ERROR: Member's View Orders page could not load.");
            }
        }

        /// <summary>
        /// Check the first row of the table for value or if no elements present
        /// set to appropriate message
        /// </summary>
        public void CheckFirstRow()
        {
            Thread.Sleep(800);
            if (HelperMethods.IsElementPresent(Driver, By.XPath(Constants.XPATH_PO_LOCATOR)))
            {
                this.FirstTableElem = Driver.FindElement(By.XPath(Constants.XPATH_PO_LOCATOR));
                if (OrderType.Equals("Draft") || OrderType.Equals("Pending Approval"))
                    this.DeleteOrderLocator = Driver.FindElement(By.XPath(Constants.XPATH_DEL_ORDER));
            }
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
            _logger.Info(" > Choosing order type: " + type);
            this.OrderType = type;
            OrderTypeDropdown.Click();
            Thread.Sleep(500);
            try
            {
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
                        throw new Exception("ViewOrdersPage: No category choosen.");
                }
                Thread.Sleep(200);
            }
            catch (ElementNotVisibleException)
            {
                _logger.Fatal(" > Choosing order type: " + type +" [FAILED]");
            }
            return this;
        }

        /// <summary>
        /// Simulates clicking the Search button
        /// </summary>
        /// <returns>Current page object</returns>
        public ViewOrdersPage InitiateSearch()
        {
            _logger.Info(" > Searching for order");
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
            _logger.Info(" > Inputting purchase order number: " + po);
            POInputTextbox.Click();
            POInputTextbox.Clear();
            POInputTextbox.SendKeys(po);
            return this;
        }

        /// <summary>
        /// Simulates deleting an order
        /// </summary>
        /// <returns>Current page object</returns>
        public ViewOrdersPage DeleteOrder()
        {
            _logger.Info(" > Attempting to delete order");
            this.AlertSuccess = false;
            DeleteOrderLocator.Click();
            
            // click OK on Information dialog
            Thread.Sleep(500);
            Driver.FindElement(By.XPath(Constants.XPATH_INFO_OK)).Click();
            this.AlertSuccess = HelperMethods.CheckAlert(Driver);
            return this;
        }
    }
}
