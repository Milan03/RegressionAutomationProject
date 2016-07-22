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

        private IWebElement ASStatus;
        private IWebElement ASPeriod;
        private IWebElement ASFrom;
        private IWebElement ASShipTo;
        private IWebElement ASSearchBtn;
        private IWebElement ASClearBtn;
        private IWebElement ASBackToBasicBtn;
        private IWebElement StatusDropdown;
        private IWebElement StatusAll;
        private IWebElement StatusUnprocessed;
        private IWebElement StatusProcessed;
        private IWebElement PeriodDropdown;
        //private IWebElement ASDateAdded;
        //private IWebElement ASPORecDate;
        //private IWebElement ASAmtFrom;
        //private IWebElement ASAmtTo;

        #region Period Years
        protected IWebElement PeriodLast90;
        protected IWebElement Period2017;
        protected IWebElement Period2016;
        protected IWebElement Period2015;
        protected IWebElement Period2014;
        protected IWebElement Period2013;
        protected IWebElement Period2012;
        protected IWebElement Period2011;
        protected IWebElement Period2010;
        protected IWebElement Period2009;
        protected IWebElement Period2008;
        #endregion

        protected IList<IWebElement> _statusDropdowns;
        protected IList<IWebElement> _periodDropdowns;
        protected IList<IWebElement> _periodListCount;

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
            _periodDropdowns = _driver.FindElements(By.XPath(Constants.PERIOD_DD_XP));
            _statusDropdowns = _driver.FindElements(By.XPath(Constants.STATUS_DD_XP));
            Thread.Sleep(500);
            if (_periodDropdowns.Any() && _statusDropdowns.Any())
            {
                PeriodDropdown = _periodDropdowns.First();
                StatusDropdown = _statusDropdowns.First();
            }
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

        private POInboxPage LoadPeriodDropdown()
        {
            _logger.Trace(" > Loading mailbox Period  dropdown...");
            _periodListCount = _driver.FindElements(By.XPath(Constants.PERIOD_COUNT_XP));
            if (_periodListCount.Count <= 4)
            {
                PeriodLast90 = HelperMethods.FindElement(_driver, "xpath", Constants.PERIOD_LAST90_XP);
                Period2016 = HelperMethods.FindElement(_driver, "xpath", Constants.PERIOD_2016_XP);
                Period2015 = HelperMethods.FindElement(_driver, "xpath", Constants.PERIOD_2015_XP);
                Period2014 = HelperMethods.FindElement(_driver, "xpath", Constants.PERIOD_2014_XP);

                if (!PeriodLast90.Equals(null) && !Period2016.Equals(null) && !Period2015.Equals(null) && !Period2014.Equals(null))
                {
                    _logger.Info(" > Load Period dropdown success!");
                    PeriodDropdownSuccess = true;
                }
            }
            return this;
        }

        /// <summary>
        /// Method to set the Period dropdown in Basic search of mailboxes. Currently only takes into
        /// account 2 years previous.
        /// </summary>
        /// <param name="period">Available periods</param>
        /// <returns>Current page object.</returns>
        internal POInboxPage SetPeriodDropdown(Constants.PeriodYear period)
        {
            _logger.Trace(" > Attempting to set search Period...");
            PeriodDropdown.Click();
            PeriodSetSuccess = false;
            switch (period)
            {
                case Constants.PeriodYear.Last90:
                    LoadPeriodDropdown();
                    PeriodLast90.Click();
                    PeriodSetSuccess = true;
                    break;
                case Constants.PeriodYear._2016:
                    LoadPeriodDropdown();
                    Period2016.Click();
                    PeriodSetSuccess = true;
                    break;
                case Constants.PeriodYear._2015:
                    LoadPeriodDropdown();
                    Period2015.Click();
                    PeriodSetSuccess = true;
                    break;
                case Constants.PeriodYear._2014:
                    LoadPeriodDropdown();
                    Period2014.Click();
                    break;
            }
            return this;
        }


        private POInboxPage LoadStatusDropdown()
        {
            _logger.Trace(" > Loading mailbox Status dropdown...");
            StatusAll = HelperMethods.FindElement(_driver, "xpath", Constants.STATUS_ALL_XP);
            StatusProcessed = HelperMethods.FindElement(_driver, "xpath", Constants.STATUS_PROCESSED_XP);
            StatusUnprocessed = HelperMethods.FindElement(_driver, "xpath", Constants.STATUS_UNPROCESSED_XP);

            if (!StatusAll.Equals(null) && !StatusProcessed.Equals(null) && !StatusUnprocessed.Equals(null))
            {
                _logger.Info(" > Mailbox Status dropdown loaded!");
                StatusDropdownSuccess = true;
            }
            return this;
        }

        /// <summary>
        /// Method to set the Status dropdown in mailbox Basic search.
        /// </summary>
        /// <param name="status">Enum w/ status states</param>
        /// <returns>Current page object</returns>
        internal POInboxPage SetSearchStatus(Constants.SearchStatus status)
        {
            _logger.Trace(" > Attempting to set search Status...");
            StatusDropdown.Click();
            StatusSetSuccess = false;
            switch (status)
            {
                case Constants.SearchStatus.All:
                    LoadStatusDropdown();
                    StatusAll.Click();
                    StatusSetSuccess = true;
                    _logger.Info(" > Search status set to All.");
                    break;
                case Constants.SearchStatus.Processed:
                    LoadStatusDropdown();
                    StatusProcessed.Click();
                    StatusSetSuccess = true;
                    _logger.Info(" > Search status set to Processed.");
                    break;
                case Constants.SearchStatus.Unprocessed:
                    LoadStatusDropdown();
                    StatusUnprocessed.Click();
                    StatusSetSuccess = true;
                    _logger.Info(" > Search status set to Unprocessed.");
                    break;
            }
            return this;
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
            ASStatus = _statusDropdowns[1]; 
            ASPeriod = _periodDropdowns[1];
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
            _periodDropdowns = _driver.FindElements(By.XPath(Constants.PERIOD_DD_XP));
            PeriodDropdown = _periodDropdowns[0];
            QuickSearchTextbox = HelperMethods.FindElement(_driver, "id", Constants.QS_TEXTBOX_ID);
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
            _poRecCheckboxes = _driver.FindElements(By.XPath(Constants.PO_CHECKBOXES_XP));
            _poRecStatuses = _driver.FindElements(By.XPath(Constants.PO_STATUSES_XP));
            _poRecSenderNames = _driver.FindElements(By.XPath(Constants.PO_SENDER_NAMES_XP));
            _poRecShipToNames = _driver.FindElements(By.XPath(Constants.PO_SHIP_TOS_XP));
            _poRecPONumbers = _driver.FindElements(By.XPath(Constants.PO_PONUMBERS_XP));
            _poRecPODates = _driver.FindElements(By.XPath(Constants.PO_PODATES_XP));
            _poRecBillToNames = _driver.FindElements(By.XPath(Constants.PO_BILL_TO_NAMES_XP));
            _poRecTotalAmounts = _driver.FindElements(By.XPath(Constants.PO_TOTAL_AMOUNTS_XP));
            _poRecDateAdded = _driver.FindElements(By.XPath(Constants.PO_DATES_ADDED_XP));

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
