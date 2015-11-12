using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using NLog;

using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;

namespace DocumentCentreTests
{
    [TestFixture]
    public class DocumentCentreTests
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #region Setup and Teardown

        private IWebDriver Driver;

        [SetUp]
        public void LoadDriver()
        {
            Driver = new FirefoxDriver();
            Driver.Navigate().GoToUrl("http://portal.test-web01.lbmx.com/login?redirect=%2f");
        }

        [TearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        #endregion

        #region Tests

        /// <summary>Test if Member login works
        [Test]
        public void MemberLoginWorks()
        {
            LoadDriver();
            logger.Info("Starting MemberLoginWorks test...");
            
            // attempt a login
            LoginPage newLogin = new LoginPage(Driver, "member");
            HomePage mem = newLogin.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);

            CleanUp();
        }

        /// <summary>Testing member login fail
        [Test]
        public void MemberLoginFails()
        {
            LoadDriver();

            // attempt a login
            LoginPage newLogin = new LoginPage(Driver, "member");
            newLogin.SubmitLoginExpectingFailure();

            // check error messsage
            var error = HelperMethods.FindElement(Driver, "classname", "login-error-message");
            Assert.AreEqual(error.Text, Constants.LOGIN_ERROR_MSG);
        }

        /// <summary>Testing member order search
        [Test]
        public void MemberOrdersSearch()
        {
            LoadDriver();

            // login as member
            LoginPage newLogin = new LoginPage(Driver, "member");
            HomePage page = newLogin.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);

            ViewOrdersPage voPage = page.NavigateToViewOrders();
            voPage.InputPurchaseOrder(Constants.ORDER_PO_PROC);
            voPage.SearchOrders(Constants.ORDER_SEARCH_PROC);
            Assert.IsNotNull(voPage);

            CleanUp();
        }

        #endregion
    }
}
