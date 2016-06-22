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
        protected internal IWebElement StatusDropdown;
        protected internal IWebElement PeriodDropdown;
        protected internal IWebElement QuickSearchTextbox;
        protected internal IWebElement QuickSearchBtn;
        protected internal IWebElement PrintBtn;
        protected internal IWebElement MarkProcBtn;
        protected internal IWebElement ASButton;
        protected internal IWebElement ResultGrid;
        #endregion

        #region More Dropdown
        protected internal IWebElement ActionsBtn;
        protected internal IWebElement MDMarkProc;
        protected internal IWebElement MDMarkUnproc;
        protected internal IWebElement MDPrintList;
        protected internal IWebElement MDOptions;
        #endregion

        protected internal IList<IWebElement> StatusDropdowns;
        protected internal IList<IWebElement> PeriodDropdowns;

        protected internal BaseInboxPage(IWebDriver driver)
        {
            _driver = driver;
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
    }
}
