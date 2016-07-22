using DocumentCentreTests.Models;
using DocumentCentreTests.Util;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class CommonInboxPage : BaseInboxPage
    {
        #region Web Elements
        protected IWebElement StatusDropdown;
        protected IWebElement StatusAll;
        protected IWebElement StatusUnprocessed;
        protected IWebElement StatusProcessed;
        protected IWebElement PeriodDropdown;

        protected IList<IWebElement> _statusDropdowns;
        protected IList<IWebElement> _periodDropdowns;
        protected IList<IWebElement> _periodListCount;

        internal bool PageReached;
        internal bool AdvLoadSuccess;
        internal bool BasicLoadSuccess;
        internal bool GridLoadSuccess;
        internal bool StatusDropdownSuccess;
        internal bool StatusSetSuccess;
        internal bool PeriodDropdownSuccess;
        internal bool PeriodSetSuccess;

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

        #endregion
        public CommonInboxPage(IWebDriver driver) : base(driver)
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
            if (!_driver.Url.Contains("Invoice"))
            {
                _logger.Fatal(" > Inbox navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            else
            {
                _logger.Info(" > Invoice Inbox page reached.");
                PageReached = true;
            }
        }

        private CommonInboxPage LoadPeriodDropdown()
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
        internal CommonInboxPage SetPeriodDropdown(Constants.PeriodYear period)
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


        private CommonInboxPage LoadStatusDropdown()
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
        internal CommonInboxPage SetSearchStatus(Constants.SearchStatus status)
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
    }


}
