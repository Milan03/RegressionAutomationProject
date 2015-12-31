using Machine.Specifications;
using NLog;
using NUnit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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

        internal static void TakeScreenshot(string saveFileName)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)_driver).GetScreenshot();
                string n = string.Format(saveFileName +"-{0:yyyy-MM-dd_hh-mm-ss-tt}.jpg",
                                            DateTime.Now);
                ss.SaveAsFile(@"C:\logging\screenshots\" + n, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        Cleanup after = () => _driver.Quit();
    }
}
