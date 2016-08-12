using DocumentCentreTests.Models;
using DocumentCentreTests.Util;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class POInboxPage : BaseInboxPage
    {
        #region Web Elements
        private List<PurchaseOrderReceived> _poRecLineItems;
        private IList<IWebElement> _poRecCheckboxes;
        private IList<IWebElement> _poRecStatuses;
        private IList<IWebElement> _poRecSenderNames;
        private IList<IWebElement> _poRecShipToNames;
        private IList<IWebElement> _poRecPONumbers;
        private IList<IWebElement> _poRecPODates;
        private IList<IWebElement> _poRecBillToNames;
        private IList<IWebElement> _poRecTotalAmounts;
        private IList<IWebElement> _poRecDateAdded;

        //private IWebElement ASStatus;
        private IWebElement ASPeriod;
        private IWebElement ASFrom;
        private IWebElement ASShipTo;
        private IWebElement ASSearchBtn;
        private IWebElement ASClearBtn;
        private IWebElement ASBackToBasicBtn;
        //private IWebElement ASDateAdded;
        //private IWebElement ASPORecDate;
        //private IWebElement ASAmtFrom;
        //private IWebElement ASAmtTo;


        #endregion

        internal bool PageReached;
        internal bool AdvLoadSuccess;
        internal bool BasicLoadSuccess;
        internal bool GridLoadSuccess;
        public POInboxPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            PageReached = false;
            AdvLoadSuccess = false;
            BasicLoadSuccess = true;
            GridLoadSuccess = false;
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
            ASPeriod = _periodDropdowns[1];
            ASFrom = HelperMethods.FindElement(_driver, Constants.SearchType.XPATH, Constants.POMailbox.XP.AS_FROM);
            ASShipTo = HelperMethods.FindElement(_driver, Constants.SearchType.XPATH, Constants.POMailbox.XP.AS_SHIP_TO);
            ASSearchBtn = HelperMethods.FindElement(_driver, Constants.SearchType.ID, Constants.BaseMailbox.ID.AS_SEARCH_BTN);
            ASClearBtn = HelperMethods.FindElement(_driver, Constants.SearchType.ID, Constants.BaseMailbox.ID.AS_CLEAR_BTN);
            ASBackToBasicBtn = HelperMethods.FindElement(_driver, Constants.SearchType.ID, Constants.BaseMailbox.ID.AS_BASIC_BTN);
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
            _periodDropdowns = _driver.FindElements(By.XPath(Constants.BaseMailbox.XP.PERIOD_DD));
            PeriodDropdown = _periodDropdowns[0];
            QuickSearchTextbox = HelperMethods.FindElement(_driver, Constants.SearchType.ID, Constants.BaseMailbox.ID.QS_TEXTBOX);
            if (!PeriodDropdown.Equals(null) && !QuickSearchBtn.Equals(null))
            {
                _logger.Info(" > PO Inbox Basic Search Loaded!");
                AdvLoadSuccess = false;
                BasicLoadSuccess = true;
            }
            return this;
        }

        /// <summary>
        /// Loading of grid row elements. Default columns are parsed; if they contain any information,
        /// it is added as a property to PurchaseOrderReceived object. Objects are contained in
        /// _poRecLineItems.
        /// </summary>
        /// <returns>Current page object.</returns>
        internal POInboxPage LoadGridRows()
        {
            _poRecLineItems = new List<PurchaseOrderReceived>();
            _poRecCheckboxes = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_CHECKBOXES));
            _poRecStatuses = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_STATUSES));
            _poRecSenderNames = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_SENDER_NAMES));
            _poRecShipToNames = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_SHIP_TOS));
            _poRecPONumbers = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_PONUMBERS));
            _poRecPODates = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_PODATES));
            _poRecBillToNames = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_BILL_TO_NAMES));
            _poRecTotalAmounts = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_TOTAL_AMOUNTS));
            _poRecDateAdded = _driver.FindElements(By.XPath(Constants.POMailbox.XP.PO_DATES_ADDED));

            try
            {
                _logger.Trace(" > Attempting to load PO Received grid elements...");
                for (int i = 0; i < _poRecPONumbers.Count; ++i)
                {
                    PurchaseOrderReceived newPO = new PurchaseOrderReceived();
                    newPO.Checkbox = _poRecCheckboxes[i];
                    newPO.Status = _poRecStatuses[i].Text;
                    newPO.SenderName = _poRecSenderNames[i].Text;
                    newPO.ShipToName = _poRecShipToNames[i].Text;
                    newPO.PONumber = _poRecPONumbers[i].Text;
                    newPO.PODate = DateTime.Parse(_poRecPODates[i].Text);
                    newPO.BillToName = _poRecBillToNames[i].Text;
                    newPO.TotalAmount = decimal.Parse(_poRecTotalAmounts[i].Text);
                    newPO.DateAdded = DateTime.Parse(_poRecDateAdded[i].Text);
                    _poRecLineItems.Add(newPO);
                }
            }
            catch (Exception e)
            {
                _logger.Fatal(" > Loading PO Received grid elements [FAILED]");
                _logger.Error(e);
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }

            if (_poRecLineItems.Any())
            {
                _logger.Info(" > Loading of PO Received grid elements complete!");
                GridLoadSuccess = true;
            }
            return this;
        }
    }
}
