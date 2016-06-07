using Machine.Specifications;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
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
        private static readonly object _locker = new object();
        private static bool _intialized = false;
        private static string logActive;

        /// <summary>
        /// Spins up an instance of FireFox webdriver which controls the browser using a
        /// FireFox plugin using a stripped down FireFox Profile.
        /// </summary>
        protected static void LoadDriver()
        {
            try
            {
                LoggingSetup();
                _logger.Info("Initiating browser driver...");
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

        internal static void LoggingSetup()
        {
            lock (_locker)
            {
                //Check if it has already run
                if (_intialized)
                {
                    return;
                }
                // Step 1. Create configuration object 
                var config = new LoggingConfiguration();

                // Step 2. Create targets and add them to the configuration 
                var consoleTarget = new ColoredConsoleTarget();
                config.AddTarget("console", consoleTarget);

                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);

                // Step 3. Set target properties 
                consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${message}";
                fileTarget.FileName = "c:/logging/" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + ".txt";
                fileTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";

                // Step 4. Define rules
                var rule1 = new LoggingRule("*", NLog.LogLevel.Debug, consoleTarget);
                config.LoggingRules.Add(rule1);

                var rule2 = new LoggingRule("*", NLog.LogLevel.Debug, fileTarget);
                config.LoggingRules.Add(rule2);

                // Step 5. Activate the configuration
                LogManager.Configuration = config;

                //Mark as run
                _intialized = true;
            }
        }

        Cleanup after = () => _driver.Quit();
    }
}
