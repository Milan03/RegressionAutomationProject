using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using DocumentCentreTests.Util;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    /// <summary>class representing Doc Centre Member Portal</summary>
    public class MemberHomePage : HomePage
    {
        private IWebElement OrdersDropdown;

        internal bool PageReached;

        /// <summary>
        /// Member home page constructor
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public MemberHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageReached = false;
            OrdersDropdown = HelperMethods.FindElement(driver, "linktext", "My Orders");
            // check if on correct page
            if (OrdersDropdown.Equals(null))
            {
                _logger.Fatal(" > Member Homepage navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            else
            {
                _logger.Info(" > Member Homepage reached!");
                _logger.Info(" > Login successful");
                PageReached = true;
            }
        }

        /// <summary>
        /// Logic to navigate to View Orders page
        /// </summary>
        /// <returns>New page object representing the destination.</returns>
        public ViewOrdersPage NavigateToOrders(string linktext, string orderType)
        {
            _logger.Trace(" > Attempting to navigate to View Orders...");

            // dropdown interaction
            OrdersDropdown.Click();
            Thread.Sleep(500);
            HelperMethods.FindElement(_driver, "linktext", linktext).Click();
            
            // check if on correct page
            if (!"My Orders".Equals(_driver.Title))
            {
                _logger.Fatal(" > View Orders page navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }

            _logger.Info(" > View Orders page reached");
            return new ViewOrdersPage(_driver, orderType);
        }

        public CataloguesPage NavigateToCatalogues()
        {
            _logger.Trace(" > Attempting to navigate to Catalogues Page...");

            OrdersDropdown.Click();
            Thread.Sleep(500);
            HelperMethods.FindElement(_driver, "linktext", "Order from Catalog").Click();

            // check if on correct page
            if (!Constants.CAT_PAGE_TITLE.Equals(_driver.Title))
            {
                _logger.Fatal(" > Catalogue navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            return new CataloguesPage(_driver);
        }
    }
}
