using OpenQA.Selenium;

namespace DocumentCentreTests.Pages
{
    public class POInboxPage : BaseInboxPage
    {
        //private IWebDriver _driver;

        private IWebElement AdvSearchStatus { get; set; }
        private IWebElement AdvSearchPeriod { get; set; }
        private IWebElement AdvSearchPOType { get; set; }
        private IWebElement AdvSearchDateAdded { get; set; }
        private IWebElement AdvSearchFrom { get; set; }
        private IWebElement AdvSearchShipTo { get; set; }

        public POInboxPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            if (!_driver.Url.Contains("PurchaseOrderReceived"))
            {
                _logger.Fatal(" > MyCart navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            else
                _logger.Info(" > Purchase Order Received page reached.");
        }
    }
}
