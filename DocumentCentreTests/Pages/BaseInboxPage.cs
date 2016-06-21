using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;

namespace DocumentCentreTests.Pages
{
    public class BaseInboxPage
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();
        protected static IWebDriver _driver;

        #region Main UI Controls
        protected internal IWebElement StatusDropdown { get; set; }
        private IWebElement PeriodDropdown { get; set; }
        private IWebElement QuickSearchTextbox { get; set; }
        private IWebElement QuickSearchBtn { get; set; }
        private IWebElement PrintBtn { get; set; }
        private IWebElement MarkProcBtn { get; set; }
        private IWebElement AdvSearchBtn { get; set; }
        private IWebElement ResultGrid { get; set; }
        #endregion

        #region More Dropdown
        private IWebElement ActionsBtn { get; set; }
        private IWebElement MDMarkProc { get; set; }
        private IWebElement MDMarkUnproc { get; set; }
        private IWebElement MDPrintList { get; set; }
        private IWebElement MDOptions { get; set; }
        #endregion

        protected internal BaseInboxPage(IWebDriver driver)
        {
            _driver = driver;
            StatusDropdown = HelperMethods.FindElement(_driver, "id", Constants.STATUS_DD_ID);
            PeriodDropdown = HelperMethods.FindElement(_driver, "id", Constants.PERIOD_DD_ID);
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
