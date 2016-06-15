using Machine.Specifications;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Edge;
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

        /// <summary>
        /// Spins up an instance of FireFox webdriver which controls the browser using a
        /// FireFox plugin using a stripped down FireFox Profile.
        /// </summary>
        protected static void LoadDriver()
        {
            try
            {
                _driver = new ChromeDriver();
                _driver.Navigate().GoToUrl("http://portal.test-web01.lbmx.com/login?redirect=%2f");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Takes a screenshot stamped w/ current date/time and is saved to c:\logging\screenshots
        /// </summary>
        /// <param name="saveFileName">desired save file name</param>
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
