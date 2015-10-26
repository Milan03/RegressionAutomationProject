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

        public MemberHomePage(IWebDriver driver)
        {
            this.driver = driver;

            // check if on correct page
            if (!"Document Centre".Equals(driver.Title))
            {
                // TODO: logic to navigate back to login page
                throw new InvalidOperationException("Login page not found");
            }
        }

        public override void NavigateToPage(IWebDriver driver)
        {

        }
    }
}
