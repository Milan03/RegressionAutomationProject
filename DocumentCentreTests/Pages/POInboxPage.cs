using DocumentCentreTests.Models;
using DocumentCentreTests.Util;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class POInboxPage : BaseInboxPage
    {
        internal List<PurchaseOrderReceived> _poRecLineItems;
        internal IList<IWebElement> _poRecCheckboxes;
        internal IList<IWebElement> _poRecStatuses;
        internal IList<IWebElement> _poRecSenderNames;
        internal IList<IWebElement> _poRecShipToName;
        internal IList<IWebElement> _poRecPONumbers;
        internal IList<IWebElement> _poRecPODates;
        internal IList<IWebElement> _poRecBillToNames;
        internal IList<IWebElement> _poRecTotalAmounts;
        internal IList<IWebElement> _poRecDateAdded;

        private IWebElement ASStatus;
        private IWebElement ASPeriod;
        private IWebElement ASFrom;
        private IWebElement ASShipTo;
        private IWebElement ASSearchBtn;
        private IWebElement ASClearBtn;
        private IWebElement ASBackToBasicBtn;
        private IWebElement ASDateAdded;
        private IWebElement ASPORecDate;
        private IWebElement ASAmtFrom;
        private IWebElement ASAmtTo;

        internal bool PageReached;
        internal bool AdvLoadSuccess;
        internal bool BasicLoadSuccess;
        public POInboxPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            PageReached = false;
            AdvLoadSuccess = false;
            BasicLoadSuccess = true;
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

        /// <summary>
        /// Loads advanced search options for this mailbox. Clicks the Advanced Search link
        /// and loads the corresponding elements.
        /// </summary>
        /// <returns>Current page element.</returns>
        internal POInboxPage LoadAdvancedSearch()
        {
            _logger.Trace(" > Attempting to load PO Inbox Advanced Search...");
            ASButton.Click();
            Thread.Sleep(500);
            //TODO: update after changes pushed
            //ASStatus = StatusDropdowns[1]; 
            ASPeriod = PeriodDropdowns[1];
            ASFrom = HelperMethods.FindElement(_driver, "xpath", Constants.AS_FROM_XP);
            ASShipTo = HelperMethods.FindElement(_driver, "xpath", Constants.AS_SHIP_TO_XP);
            ASSearchBtn = HelperMethods.FindElement(_driver, "id", Constants.AS_SEARCH_BTN_ID);
            ASClearBtn = HelperMethods.FindElement(_driver, "id", Constants.AS_CLEAR_BTN_ID);
            ASBackToBasicBtn = HelperMethods.FindElement(_driver, "id", Constants.AS_BASIC_BTN_ID);
            if (!ASPeriod.Equals(null) && !ASFrom.Equals(null) && !ASShipTo.Equals(null) &&
                !ASSearchBtn.Equals(null) && !ASClearBtn.Equals(null) && !ASBackToBasicBtn.Equals(null))
            {
                _logger.Info(" > PO Inbox Advanced Search Loaded!");
                AdvLoadSuccess = true;
                BasicLoadSuccess = false;
            }
            return this;
        }

        /// <summary>
        /// Loads basic search options if on Advanced Search options. Clicks the Basic Search
        /// link and loads the corresponding elements.
        /// </summary>
        /// <returns>Current page item.</returns>
        internal POInboxPage LoadBasicSearch()
        {
            _logger.Trace(" > Attempting to load PO Inbox Basic Search...");
            ASBackToBasicBtn.Click();
            Thread.Sleep(500);
            //TODO: update after changes pushed
            //ASStatus = StatusDropdowns[1]; 
            PeriodDropdowns = _driver.FindElements(By.XPath(Constants.PERIOD_DD_XP));
            PeriodDropdown = PeriodDropdowns.First();
            QuickSearchTextbox = HelperMethods.FindElement(_driver, "id", Constants.QS_TEXTBOX_ID);
            if (!PeriodDropdown.Equals(null) && !QuickSearchBtn.Equals(null))
            {
                _logger.Info(" > PO Inbox Basic Search Loaded!");
                AdvLoadSuccess = false;
                BasicLoadSuccess = true;
            }
            return this;
        }
    }
}
