using DocumentCentreTests.Util;
using OpenQA.Selenium;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class POInboxPage : BaseInboxPage
    {
        private IWebElement AdvSearchStatus;
        private IWebElement AdvSearchPeriod;
        private IWebElement AdvSearchDateAdded;
        private IWebElement AdvSearchPORecDate;
        private IWebElement AdvSearchAmtFrom;
        private IWebElement AdvSearchAmtTo;
        private IWebElement AdvSearchFrom;
        private IWebElement AdvSearchShipTo;

        internal bool PageReached;
        internal bool AdvLoadSuccess;
        public POInboxPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            PageReached = false;
            AdvLoadSuccess = false;
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

        internal POInboxPage LoadAdvSearch()
        {
            AdvSearchBtn.Click();
            Thread.Sleep(500);
            //AdvSearchStatus = StatusDropdowns[1]; //TODO: update after changes pushed
            AdvSearchPeriod = PeriodDropdowns[1];
            AdvSearchFrom = HelperMethods.FindElement(_driver, "xpath", Constants.AS_FROM_XP);
            AdvSearchShipTo = HelperMethods.FindElement(_driver, "xpath", Constants.AS_SHIP_TO_XP);

            if (!AdvSearchPeriod.Equals(null))
                AdvLoadSuccess = true;

            AdvSearchStatus.Click();
            return this;
        }
    }
}
