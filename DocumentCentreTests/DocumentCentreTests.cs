using Gallio.Framework;
using Gallio.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;

namespace DocumentCentreTests
{
    [TestFixture]
    public class DocumentCentreTests
    {
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

        /// <summary>
        /// Test if Member login works
        /// </summary>
        //[Test]
        //public void MemberLoginWorks()
        //{
        //    LoadDriver();
        //    Driver.Navigate().GoToUrl("http://portal.test-web01.lbmx.com/login?redirect=%2f");

        //    // attempt a login
        //    LoginPage newLogin = new LoginPage(Driver, "member");
        //    HomePage mem = newLogin.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);

        //    CleanUp();
        //}

        [Test]
        public void MemberOrdersSearch()
        {

            LoadDriver();

            // login as member
            LoginPage newLogin = new LoginPage(Driver, "member");
            HomePage page = newLogin.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);

            ViewOrdersPage voPage = page.NavigateToViewOrders();
            voPage.SearchDraftOrders();
            Assert.IsNotNull(voPage);

            CleanUp();
        }

        #endregion
    }
}
