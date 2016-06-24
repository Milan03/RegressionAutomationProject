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
        #endregion

        protected IList<IWebElement> StatusDropdowns;
        protected IList<IWebElement> PeriodDropdowns;

        protected internal bool NavRowSuccess;
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

            // verification
            if(!FirstPageBtn.Equals(null) && !PrevPageBtn.Equals(null) && !NextPageBtn.Equals(null)
                && !LastPageBtn.Equals(null) && !_navPages.Any())
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
            LoadNavigationRow();
            FirstPageBtn.Click();
            return this;
        }
        protected internal BaseInboxPage NavToPrevPage()
        {
            LoadNavigationRow();
            PrevPageBtn.Click();
            return this;
        }
        protected internal BaseInboxPage NavToNextPage()
        {
            LoadNavigationRow();
            NextPageBtn.Click();
            return this;
        }

        protected internal BaseInboxPage NavToLastPage()
        {
            LoadNavigationRow();
            LastPageBtn.Click();
            return this;
        }
    }
}
