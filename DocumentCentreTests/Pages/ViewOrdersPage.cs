using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests.Pages
{
    public class ViewOrdersPage
    {
        private IWebDriver driver;

        /// <summary>
        /// Class representing View Orders for Members
        /// </summary>
        /// <param name="driver">Main interface for testing, represents idealised web browser</param>
        public ViewOrdersPage(IWebDriver driver)
        {
            this.driver = driver;

            // check if on correct page
            if (!"My Orders".Equals(driver.Title))
            {
                throw new NoSuchWindowException("View Orders page not found");
            }
        }

        
    }
}
