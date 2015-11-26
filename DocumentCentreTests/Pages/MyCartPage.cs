using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Pages
{
    public class MyCartPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;

        public MyCartPage(IWebDriver driver)
        {
            this.Driver = driver;
        }
    }
}
