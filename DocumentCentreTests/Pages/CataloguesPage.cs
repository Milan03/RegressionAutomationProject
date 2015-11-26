using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Pages
{
    public class CataloguesPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;
        private IWebElement SearchInputLocator;
        private IWebElement SearchButtonLocator;

        /// <summary>
        /// Page Object representing the view Catalogues page
        /// </summary>
        /// <param name="driver">Main interface for testing</param>
        public CataloguesPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.SearchInputLocator = HelperMethods.FindElement(driver, "id", "searchTerm");
            this.SearchButtonLocator = HelperMethods.FindElement(driver, "id", "catalogSearchButton");

            if (!Constants.CAT_PAGE_TITLE.Equals(driver.Title))
            {
                _logger.Fatal("       - Member's Catalogue page not found.");
                throw new NoSuchWindowException("Member's Catalogue page not found.");
            }
        }

        /// <summary>
        /// Simulates inputting a catalogue to search for
        /// </summary>
        /// <param name="catalogue">name of catalogue</param>
        /// <returns>current page object</returns>
        public CataloguesPage InputCatalogueName(string catalogue)
        {
            _logger.Info("       - Inputting catalogue name for search: " + catalogue);
            SearchInputLocator.Clear();
            SearchInputLocator.SendKeys(catalogue);
            return this;
        }

        /// <summary>
        /// Simulates clicking the Search button for catalogues
        /// </summary>
        /// <returns>current page object</returns>
        public CataloguesPage InitiateSearch()
        {
            _logger.Info("       - Searching for catalogue...");
            SearchButtonLocator.Click();
            return this;
        }
    }
}
