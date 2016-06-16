using NLog;
using OpenQA.Selenium;

namespace DocumentCentreTests.Pages
{
    public class BaseInboxPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        #region Main UI Controls
        private IWebElement HeaderLbl { get; set; }
        private IWebElement SubHeaderLbls { get; set; }
        private IWebElement StatusDropdown { get; set; }
        private IWebElement PeriodDropdown { get; set; }
        private IWebElement QuickSearchTextbox { get; set; }
        private IWebElement QuickSearchBtn { get; set; }
        private IWebElement PrintBtn { get; set; }
        private IWebElement MarkProcBtn { get; set; }
        private IWebElement AdvSearchBtn { get; set; }
        private IWebElement ResultGrid { get; set; }
        #endregion

        #region More Dropdown
        private IWebElement MoreDropdown { get; set; }
        private IWebElement MDMarkProc { get; set; }
        private IWebElement MDMarkUnproc { get; set; }
        private IWebElement MDPrintList { get; set; }
        private IWebElement MDOptions { get; set; }
        #endregion

        protected internal BaseInboxPage(IWebDriver driver)
        {

        }
    }
}
