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

        protected internal enum GridElementsToDisplay
        {
            Ten = 10,
            Twenty = 20,
            Fifty = 50,
            Hundred = 100
        };

        protected IList<IWebElement> StatusDropdowns;
        protected IList<IWebElement> PeriodDropdowns;

        protected internal bool NavRowSuccess;
        protected internal bool GridAmountDropdownSuccess;
        protected internal bool GridAmountSetSuccess;
        protected internal BaseInboxPage(IWebDriver driver)
        {
            _driver = driver;
            NavRowSuccess = false;
            Thread.Sleep(500);
            PeriodDropdowns = _driver.FindElements(By.XPath(Constants.PERIOD_DD_XP));
            PeriodDropdown = PeriodDropdowns.First();
            //StatusDropdown = HelperMethods.FindElement(_driver, "id", Constants.STATUS_DD_XP);
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

        protected internal BaseInboxPage NavToFirstPage()
        {
            _logger.Trace(" > Navigating to first page of grid...");
            LoadNavigationRow();
            FirstPageBtn.Click();
            return this;
        }
        protected internal BaseInboxPage NavToPrevPage()
        {
            _logger.Trace(" > Navigating to previous page of grid...");
            LoadNavigationRow();
            PrevPageBtn.Click();
            return this;
        }
        protected internal BaseInboxPage NavToNextPage()
        {
            _logger.Trace(" > Navigating to next page of grid...");
            LoadNavigationRow();
            NextPageBtn.Click();
            return this;
        }

        protected internal BaseInboxPage NavToLastPage()
        {
            _logger.Trace(" > Navigating to last page of grid...");
            LoadNavigationRow();
            LastPageBtn.Click();
            return this;
        }

        protected internal BaseInboxPage LoadGridItemAmountDropdown()
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

        protected internal BaseInboxPage SetGridItemAmount(GridElementsToDisplay toDisplay)
        {
            LoadNavigationRow();
            GridItemAmountDropdown.Click();
            GridAmountSetSuccess = false;
            switch (toDisplay)
            {
                case GridElementsToDisplay.Ten:
                    LoadGridItemAmountDropdown();
                    GridItemAmt10.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 10 items max.");
                    break;
                case GridElementsToDisplay.Twenty:
                    LoadGridItemAmountDropdown();
                    GridItemAmt20.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 20 items max.");
                    break;
                case GridElementsToDisplay.Fifty:
                    LoadGridItemAmountDropdown();
                    GridItemAmt50.Click();
                    GridAmountSetSuccess = true;
                    _logger.Info(" > Mailbox grid set to display 50 items max.");
                    break;
                case GridElementsToDisplay.Hundred:
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
