using OpenQA.Selenium;

namespace DocumentCentreTests.Pages
{
    public class POInboxPage : BaseInboxPage
    {
        private IWebElement AdvSearchStatus;
        private IWebElement AdvSearchPeriod;
        private IWebElement AdvSearchPOType;
        private IWebElement AdvSearchDateAdded;
        private IWebElement AdvSearchFrom;
        private IWebElement AdvSearchShipTo;

        internal bool PageReached;
        public POInboxPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            PageReached = false;
            if (!_driver.Url.Contains("PurchaseOrderReceived"))
            {
                _logger.Fatal(" > MyCart navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            else
            {
                _logger.Info(" > Purchase Order Received page reached.");
                PageReached = true;
            }
        }
    }
}
