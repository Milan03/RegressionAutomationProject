using OpenQA.Selenium;
using System;
using DocumentCentreTests.Util;
using NLog;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class ViewOrdersPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        private IWebElement OrderTypeDropdown;
        private IWebElement POInputTextbox;
        private IWebElement SearchOrdersButton;
        private IWebElement DeleteOrderLocator;
        private IWebElement CreateEditLocator;
        private IWebElement EditOrderLocator;

        internal IWebElement FirstTableElem { get; set;  }
        internal bool AlertSuccess { get; set; }
        internal string OrderType { get; set; }

        public ViewOrdersPage(IWebDriver driver, string orderType)
        {
            _logger.Info(" > View Orders Page is being constructed...");
            _driver = driver;
            OrderTypeDropdown = HelperMethods.FindElement(this._driver, "classname", "k-widget");
            POInputTextbox = HelperMethods.FindElement(this._driver, "id", "poNumber");
            SearchOrdersButton = HelperMethods.FindElement(this._driver, "id", "searchOrdersButton");
            OrderType = orderType;

            // get first table element
            CheckFirstRow();

            // check if on correct page
            if (!"My Orders".Equals(_driver.Title))
            {
                _logger.Fatal(" > ViewOrders navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            } else
                _logger.Info(" > View Orders page reached!");
        }

        /// <summary>
        /// Check the first row of the table for value or if no elements present
        /// set to appropriate message
        /// </summary>
        public void CheckFirstRow()
        {
            Thread.Sleep(800);
            if (HelperMethods.IsElementPresent(_driver, By.XPath(Constants.PO.XP.PO_LOCATOR)))
            {
                FirstTableElem = _driver.FindElement(By.XPath(Constants.PO.XP.PO_LOCATOR));
                if (OrderType.Equals(Constants.OrderStatus.DRAFT) || OrderType.Equals("Pending Approval"))
                {
                    DeleteOrderLocator = _driver.FindElement(By.XPath(Constants.PO.XP.DEL_ORDER));
                    CreateEditLocator = _driver.FindElement(By.XPath(Constants.PO.XP.RECREATE_ORDER));
                    EditOrderLocator = _driver.FindElement(By.XPath(Constants.PO.XP.EDIT_ORDER));
                }
            }
            else // no table elements found, set to message
                FirstTableElem = _driver.FindElement(By.XPath("id('ordersGrid')/div[2]/div[2]"));    
        }

        /// <summary>
        /// For simulating order searchs by members
        /// </summary>
        /// <param name="type">Type of order to search for</param>
        /// <returns>Current page object</returns>
        public ViewOrdersPage ChooseOrderType(string type)
        {
            _logger.Info(" > Choosing order type: " + type);
            OrderType = type;
            OrderTypeDropdown.Click();
            Thread.Sleep(500);
            try
            {
                switch (type)
                {
                    case Constants.OrderStatus.DRAFT:
                        _driver.FindElement(By.XPath("id('orderStatus_listbox')/li[2]")).Click();
                        break;
                    case "Pending":
                        _driver.FindElement(By.XPath("id('orderStatus_listbox')/li[3]")).Click();
                        break;
                    case "Sent":
                        _driver.FindElement(By.XPath("id('orderStatus_listbox')/li[4]")).Click();
                        break;
                    case "Processing":
                        _driver.FindElement(By.XPath("id('orderStatus_listbox')/li[5]")).Click();
                        break;
                    case "Delivered":
                        _driver.FindElement(By.XPath("id('orderStatus_listbox')/li[6]")).Click();
                        break;
                    default:
                        throw new Exception("ViewOrdersPage: No category choosen.");
                }
                Thread.Sleep(200);
            }
            catch (ElementNotVisibleException)
            {
                _logger.Fatal(" > Choosing Order Type [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            return this;
        }

        public ViewOrdersPage InitiateSearch()
        {
            _logger.Trace(" > Searching for order...");
            SearchOrdersButton.Click();
            return this;
        }

        public ViewOrdersPage InputPurchaseOrder(string po)
        {
            _logger.Trace(" > Inputting purchase order number: " + po);
            POInputTextbox.Click();
            POInputTextbox.Clear();
            POInputTextbox.SendKeys(po);
            return this;
        }

        public ViewOrdersPage DeleteOrder()
        {
            _logger.Trace(" > Attempting to delete order...");
            AlertSuccess = false;
            DeleteOrderLocator.Click();
            
            // click OK on Information dialog
            Thread.Sleep(500);
            _driver.FindElement(By.XPath(Constants.PO.XP.INFO_OK)).Click();
            AlertSuccess = HelperMethods.CheckAlert(_driver);
            return this;
        }

        public MyCartPage ReCreateOrder(string PONumber)
        {
            _logger.Trace(" > Attempting to recreate PO: " + PONumber + "...");
            InputPurchaseOrder(PONumber);
            InitiateSearch();
            OrderType = Constants.OrderStatus.DRAFT;
            CheckFirstRow();
            try
            {
                CreateEditLocator.Click();
                _logger.Error(" > Recreation of PO: " + PONumber + " [SUCCESS].");
            }
            catch (NullReferenceException)
            {
                _logger.Error(" > Recreation of PO: " + PONumber + " [FAILED].");
            }

            return new MyCartPage(_driver, "new_order");
        }
    }
}
