using Machine.Specifications;
using NLog;
using NUnit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests
{
    public abstract class BaseDriverTest
    {
        protected static NLog.Logger _logger = LogManager.GetCurrentClassLogger();
        protected static IWebDriver _driver;

        protected static void LoadDriver()
        {
            _driver = new FirefoxDriver();
            _driver.Navigate().GoToUrl("http://portal.test-web01.lbmx.com/login?redirect=%2f");
        }

        Cleanup after = () => _driver.Quit();
    }
}
