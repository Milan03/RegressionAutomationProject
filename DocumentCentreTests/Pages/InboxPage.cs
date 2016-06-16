using DocumentCentreTests.Catalogue;
using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Pages
{
    public class InboxPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        #region UI Controls
        private IWebElement HeaderLbl;
        private IWebElement SubHeaderLbls;
        private IWebElement StatusDropdown;
        private IWebElement PeriodDropdown;
        private IWebElement QuickSearchTextbox;
        private IWebElement QuickSearchBtn;
        private IWebElement AdvSearchBtn;
        private IWebElement PrintBtn;
        private IWebElement MarkProcBtn;

        private IWebElement MoreDropdown;
        private IWebElement MDMarkProc;
        private IWebElement MDMarkUnproc;
        private IWebElement MDPrintList;
        private IWebElement MDOptions;
        #endregion
    }
}
