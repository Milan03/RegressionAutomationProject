using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class BaseInboxPage
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();
        protected static IWebDriver _driver;

        #region Main UI Controls
        protected IWebElement StatusDropdown;
        protected IWebElement StatusAll;
        protected IWebElement StatusUnprocessed;
        protected IWebElement StatusProcessed;
        protected IWebElement PeriodDropdown;
        protected IWebElement QuickSearchTextbox;
        protected IWebElement QuickSearchBtn;
        protected IWebElement PrintBtn;
        protected IWebElement MarkProcBtn;
        protected IWebElement ASButton;
        protected IWebElement ResultGrid;

        protected IWebElement ActionsBtn;
        protected IWebElement MDMarkProc;
        protected IWebElement MDMarkUnproc;
        protected IWebElement MDPrintList;
        protected IWebElement MDOptions;

        protected IList<IWebElement> _navPages;
        protected IWebElement FirstPageBtn;
        protected IWebElement PrevPageBtn;
        protected IWebElement NextPageBtn;
        protected IWebElement LastPageBtn;
        protected IWebElement GridItemAmountDropdown;
        protected IWebElement GridItemAmt10;
        protected IWebElement GridItemAmt20;
        protected IWebElement GridItemAmt50;
        protected IWebElement GridItemAmt100;
        protected IWebElement PageLabel;
        protected IWebElement PageRefreshBtn;
        #endregion

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

        protected internal bool NavRowSuccess;
        protected internal bool GridAmountDropdownSuccess;
        protected internal bool GridAmountSetSuccess;
        protected internal bool StatusDropdownSuccess;
        protected internal bool StatusSetSuccess;
        protected internal bool PeriodDropdownSuccess;
        protected internal bool PeriodSetSuccess;
        protected internal BaseInboxPage(IWebDriver driver)
        {
            _driver = driver;
            _periodDropdowns = _driver.FindElements(By.XPath(Constants.PERIOD_DD_XP));
            _statusDropdowns = _driver.FindElements(By.XPath(Constants.STATUS_DD_XP));
            Thread.Sleep(500);
            PeriodDropdown = _periodDropdowns.First();
            StatusDropdown = _statusDropdowns.First();
            QuickSearchTextbox = HelperMethods.FindElement(_driver, "id", Constants.QS_TEXTBOX_ID);
            QuickSearchBtn = HelperMethods.FindElement(_driver, "id", Constants.QS_BTN_ID);
            ResultGrid = HelperMethods.FindElement(_driver, "id", Constants.RSLT_GRID_ID);
            PrintBtn = HelperMethods.FindElement(_driver, "id", Constants.PRINT_BTN_ID);
            MarkProcBtn = HelperMethods.FindElement(_driver, "id", Constants.MP_BTN_ID);
            ASButton = HelperMethods.FindElement(_driver, "id", Constants.AS_LINK_ID);
            ActionsBtn = HelperMethods.FindElement(_driver, "id", Constants.ACTIONS_BTN_ID);

            if (!_driver.Url.Contains("Mailbox"))
            {
                _logger.Fatal(" > Mailbox navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
        }

        private BaseInboxPage LoadPeriodDropdown()
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
        internal BaseInboxPage SetPeriodDropdown(Constants.PeriodYear period)
        {
            _logger.Trace(" > Attempting to set search Period...");
            PeriodDropdown.Click();
            PeriodSetSuccess = false;
            switch(period)
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

        private BaseInboxPage LoadStatusDropdown()
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
        internal BaseInboxPage SetSearchStatus(Constants.SearchStatus status)
        {
            _logger.Trace(" > Attempting to set search Status...");
            StatusDropdown.Click();
            StatusSetSuccess = false;
            switch(status)
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
        /// Method to load the bottom navigation row of the grid. This method is used to load
        /// the row elements before each navigation to avoid stale elements.
        /// </summary>
        /// <returns>Current page object.</returns>
        private BaseInboxPage LoadNavigationRow()
        {
            _logger.Trace(" > Attempting to load navigation row...");
            _navPages = new List<IWebElement>();
            _navPages = _driver.FindElements(By.XPath(Constants.PAGE_NUM_XP));
            FirstPageBtn = HelperMethods.FindElement(_driver, "xpath", Constants.FIRST_PAGE_NAV_XP);
            PrevPageBtn = HelperMethods.FindElement(_driver, "xpath", Constants.PREV_PAGE_NAV_XP);
            NextPageBtn = HelperMethods.FindElement(_driver, "xpath", Constants.NEXT_PAGE_NAV_XP);
            LastPageBtn = HelperMethods.FindElement(_driver, "xpath", Constants.LAST_PAGE_NAV_XP);
            GridItemAmountDropdown = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_DROPDOWN_XP);
            PageLabel = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_INFO_LBL_XP);
            PageRefreshBtn = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_REFRESH_XP);

            // verification
            if(!FirstPageBtn.Equals(null) && !PrevPageBtn.Equals(null) && !NextPageBtn.Equals(null)
                && !LastPageBtn.Equals(null) && !GridItemAmountDropdown.Equals(null) && !PageLabel.Equals(null)
                && !PageRefreshBtn.Equals(null) && !_navPages.Any())
            {
                _logger.Info(" > Mailbox navigation row loaded!");
                NavRowSuccess = true;
            } 
            else
            {
                _logger.Info(" > Problem loading mailbox navigation row!");
                NavRowSuccess = true;
            }
            return this;
        }
        internal BaseInboxPage NavToFirstPage()
        {
            _logger.Trace(" > Navigating to first page of grid...");
            LoadNavigationRow();
            FirstPageBtn.Click();
            return this;
        }
        internal BaseInboxPage NavToPrevPage()
        {
            _logger.Trace(" > Navigating to previous page of grid...");
            LoadNavigationRow();
            PrevPageBtn.Click();
            return this;
        }
        internal BaseInboxPage NavToNextPage()
        {
            _logger.Trace(" > Navigating to next page of grid...");
            LoadNavigationRow();
            NextPageBtn.Click();
            return this;
        }
        internal BaseInboxPage NavToLastPage()
        {
            _logger.Trace(" > Navigating to last page of grid...");
            LoadNavigationRow();
            LastPageBtn.Click();
            return this;
        }
        private BaseInboxPage LoadGridItemAmountDropdown()
        {
            _logger.Trace(" > Loading grid item amount dropdown...");
            GridItemAmt10 = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_AMT_10);
            GridItemAmt20 = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_AMT_20);
            GridItemAmt50 = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_AMT_50);
            GridItemAmt100 = HelperMethods.FindElement(_driver, "xpath", Constants.PAGE_AMT_100);

            if (!GridItemAmt10.Equals(null) && !GridItemAmt20.Equals(null) &&
                !GridItemAmt50.Equals(null) && !GridItemAmt100.Equals(null))
                GridAmountDropdownSuccess = true;

            return this;
        }

        /// <summary>
        /// Simulates setting the amount of elements to display in grid.
        /// </summary>
        /// <param name="toDisplay">Amount to display.</param>
        /// <returns>Current page object.</returns>
        internal BaseInboxPage SetGridItemAmount(Constants.GridElementsToDisplay toDisplay)
        {
            LoadNavigationRow();
            GridItemAmountDropdown.Click();
            GridAmountSetSuccess = false;
            switch (toDisplay)
            {
                case Constants.GridElementsToDisplay.Ten:
                    LoadGridItemAmountDropdown();
                    GridItemAmt10.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 10 items max.");
                    break;
                case Constants.GridElementsToDisplay.Twenty:
                    LoadGridItemAmountDropdown();
                    GridItemAmt20.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 20 items max.");
                    break;
                case Constants.GridElementsToDisplay.Fifty:
                    LoadGridItemAmountDropdown();
                    GridItemAmt50.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 50 items max.");
                    break;
                case Constants.GridElementsToDisplay.Hundred:
                    LoadGridItemAmountDropdown();
                    GridItemAmt100.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 100 items max.");
                    break;
            }
            return this;
        }
    }
}
