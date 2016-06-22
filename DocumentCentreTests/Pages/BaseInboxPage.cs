using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace DocumentCentreTests.Pages
{
    public class BaseInboxPage
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();
        protected static IWebDriver _driver;

        #region Main UI Controls
        protected internal IWebElement StatusDropdown { get; set; }
        protected internal IWebElement PeriodDropdown { get; set; }
        protected internal IWebElement QuickSearchTextbox { get; set; }
        protected internal IWebElement QuickSearchBtn { get; set; }
        protected internal IWebElement PrintBtn { get; set; }
        protected internal IWebElement MarkProcBtn { get; set; }
        protected internal IWebElement AdvSearchBtn { get; set; }
        protected internal IWebElement ResultGrid { get; set; }
        #endregion

        #region More Dropdown
        protected internal IWebElement ActionsBtn { get; set; }
        protected internal IWebElement MDMarkProc { get; set; }
        protected internal IWebElement MDMarkUnproc { get; set; }
        protected internal IWebElement MDPrintList { get; set; }
        protected internal IWebElement MDOptions { get; set; }
        #endregion

        protected internal IList<IWebElement> StatusDropdowns;
        protected internal IList<IWebElement> PeriodDropdowns;

        protected internal BaseInboxPage(IWebDriver driver)
        {
            _driver = driver;
            PeriodDropdowns = _driver.FindElements(By.XPath(Constants.PERIOD_DD_XP));
            PeriodDropdown = StatusDropdowns.First();
            StatusDropdown = HelperMethods.FindElement(_driver, "id", Constants.STATUS_DD_XP);
            QuickSearchTextbox = HelperMethods.FindElement(_driver, "id", Constants.QS_TEXTBOX_ID);
            QuickSearchBtn = HelperMethods.FindElement(_driver, "id", Constants.QS_BTN_ID);
            ResultGrid = HelperMethods.FindElement(_driver, "id", Constants.RSLT_GRID_ID);
            PrintBtn = HelperMethods.FindElement(_driver, "id", Constants.PRINT_BTN_ID);
            MarkProcBtn = HelperMethods.FindElement(_driver, "id", Constants.MP_BTN_ID);
            AdvSearchBtn = HelperMethods.FindElement(_driver, "id", Constants.AS_LINK_ID);
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
