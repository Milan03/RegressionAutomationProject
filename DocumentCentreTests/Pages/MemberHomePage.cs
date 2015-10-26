using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests.Pages
{
    public class MemberHomePage : HomePage
    {
        private IWebDriver driver;

        /// <summary>
        /// Member home page constructor
        /// </summary>
        /// <param name="driver"></param>
        public MemberHomePage(IWebDriver driver)
        {
            this.driver = driver;

            // check if on correct page
            if (HelperMethods.FindElement(driver, "id", "userActionsButton") == null)
            {
                // TODO: logic to navigate back to login page
                throw new InvalidOperationException("Member homepage not found");
            }
        }
    }
}
