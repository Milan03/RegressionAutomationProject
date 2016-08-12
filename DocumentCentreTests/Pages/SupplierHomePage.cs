using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using DocumentCentreTests.Util;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class SupplierHomePage : HomePage
    {
        private IWebElement OrdersDropdown;

        internal bool PageReached;

        public SupplierHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageReached = false;
            OrdersDropdown = HelperMethods.FindElement(_driver, "linktext", "Order Fulfillment");
            if (OrdersDropdown.Equals(null))
            {
                _logger.Fatal(" > Supplier Homepage navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            else
            {
                _logger.Info(" > Member Homepage reached!");
                PageReached = true;
            }
        }

        public BaseInboxPage NavigateToMailbox(string mailboxName)
        {
            switch (mailboxName)
            {
                case Constants.Text.VIEW_POS:
                    OrdersDropdown.Click();
                    Thread.Sleep(1000);
                    HelperMethods.FindElement(_driver, "linktext", Constants.Text.VIEW_POS).Click();
                    return new POInboxPage(_driver);
                default:
                    _logger.Fatal(" > Mailbox: " + mailboxName + " navigation [FAILED]");
                    _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                    BaseDriverTest.TakeScreenshot("screenshot");
                    return null;
            }
        }
    }
}
