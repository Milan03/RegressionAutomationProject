using Machine.Specifications;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace DocumentCentreTests
{
    public abstract class BaseDriverTest
    {
        protected static NLog.Logger _logger = LogManager.GetCurrentClassLogger();
        protected static IWebDriver _driver;
        protected static ChromeOptions _chromeOptions;

        /// <summary>
        /// Spins up an instance of FireFox webdriver which controls the browser using a
        /// FireFox plugin using a stripped down FireFox Profile.
        /// </summary>
        protected static void LoadDriver()
        {
            //ChromeOptions options = new ChromeOptions();
            try
            {
                //options.AddArgument("--start-maximized");
                //_driver = new ChromeDriver();
                
                //var profile = new FirefoxProfile();
                //profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream doc xls pdf txt");
                _driver = new FirefoxDriver();
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
