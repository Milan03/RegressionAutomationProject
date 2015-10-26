using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests.Pages
{
    /// <summary>tests for the sauce labs guinea pig page</summary>
    [TestFixture]
    [Header("browser", "version", "platform")] // name of the parameters in the rows
    [Row("internet explorer", "11", "Windows 7")] // run all tests in the fixture against IE 11 for windows 7
    [Row("chrome", "35", "linux")] // run all tests in the fixture against chrome 35 for linux
    [Row("safari", "6", "OS X 10.8")] // run all tests in the fixture against safari 6 and mac OS X 10.8
    public class DocumentCentreTests
    {
        #region Setup and Teardown

        /// <summary>starts a sauce labs sessions</summary>
        /// <param name="browser">name of the browser to request</param>
        /// <param name="version">version of the browser to request</param>
        /// <param name="platform">operating system to request</param>
        private IWebDriver _Setup(string browser, string version, string platform)
        {
            // construct the url to sauce labs
            Uri commandExecutorUri = new Uri("http://ondemand.saucelabs.com/wd/hub");

            // set up the desired capabilities
            DesiredCapabilities desiredCapabilites = new DesiredCapabilities(browser, version, Platform.CurrentPlatform); // set the desired browser
            desiredCapabilites.SetCapability("platform", platform); // operating system to use
            desiredCapabilites.SetCapability("username", Constants.SAUCE_LABS_ACCOUNT_NAME); // supply sauce labs username
            desiredCapabilites.SetCapability("accessKey", Constants.SAUCE_LABS_ACCOUNT_KEY);  // supply sauce labs account key
            desiredCapabilites.SetCapability("name", TestContext.CurrentContext.Test.Name); // give the test a name

            // start a new remote web driver session on sauce labs
            var _Driver = new RemoteWebDriver(commandExecutorUri, desiredCapabilites);
            _Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));

            // navigate to the page under test
            _Driver.Navigate().GoToUrl("http://portal.test-web01.lbmx.com/login?redirect=%2f");

            return _Driver;
        }


        /// <summary>called at the end of each test to tear it down</summary>
        public void CleanUp(IWebDriver _Driver)
        {
            // get the status of the current test
            bool passed = TestContext.CurrentContext.Outcome.Status == TestStatus.Passed;
            try
            {
                // log the result to sauce labs
                ((IJavaScriptExecutor)_Driver).ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            }
            finally
            {
                // terminate the remote webdriver session
                _Driver.Quit();
            }
        }

        #endregion

        #region Tests

        /// <summary>Assert Login to LBMX web01 test site works</summary>
        [Test, Parallelizable]
        public void LoginWorks(string browser, string version, string platform)
        {
            var _Driver = _Setup(browser, version, platform);
            HomePage page = null;

            // attempt a login
            LoginPage newLogin = new LoginPage(_Driver, "member");
            page = newLogin.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
            Assert.IsNotNull(page);

            CleanUp(_Driver);
        }

        /// <summary>Assert Login to LBMX web01 test site works</summary>
        //[Test, Parallelizable]
        //public void MemberOrderFromCatalog(string browser, string version, string platform)
        //{
        //    var _Driver = _Setup(browser, version, platform);
        //    // attempt a login
        //    HelperMethods.Login(_Driver, Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
        //    // navigate to orders
        //    var ordersDropdown = _Driver.FindElement(By.LinkText("My Orders"));
        //    ordersDropdown.Click();
        //    var newOrder = _Driver.FindElement(By.LinkText("Order from Catalog"));
        //    newOrder.Click();
        //    // check if correct page
        //    bool reached = false;
        //    if (_Driver.FindElement(By.Id("searchTerm")) != null)
        //    {
        //        reached = true;
        //        Assert.IsTrue(reached);
        //        // correct page
        //        CleanUp(_Driver);
        //    }
        //    else
        //    {
        //        // test failed
        //        Assert.Fail("Error in MemberOrderFromCatalog test");
        //        CleanUp(_Driver);
        //    }
        //}

        #endregion
    }
}
