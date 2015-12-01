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
        private IWebElement SearchInputTextbox;
        private IWebElement SearchButton;

        /// <summary>
        /// Page Object representing the view Catalogues page
        /// </summary>
        /// <param name="driver">Main interface for testing</param>
        public CataloguesPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.SearchInputTextbox = HelperMethods.FindElement(driver, "id", "searchTerm");
            this.SearchButton = HelperMethods.FindElement(driver, "id", "catalogSearchButton");

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
            SearchInputTextbox.Clear();
            SearchInputTextbox.SendKeys(catalogue);
            return this;
        }

        /// <summary>
        /// Simulates clicking the Search button for catalogues
        /// </summary>
        /// <returns>current page object</returns>
        public CataloguesPage InitiateSearch()
        {
            _logger.Info("       - Searching for catalogue...");
            SearchButton.Click();
            return this;
        }

        /// <summary>
        /// Simulates choosing a catalogue from the ones listed on page
        /// </summary>
        /// <param name="name">name of catalogue</param>
        /// <returns>new Product page object related to catalogue choosen</returns>
        public ProductsPage ChooseCatalogue(string name)
        {
            IWebElement catalogueTitle = HelperMethods.FindElement(Driver, "xpath", "//h1[contains(@class, 'catalog-tile-text') and contains (text(), '"+name+"')]");
            catalogueTitle.Click();
            return new ProductsPage(Driver);
        }
    }
}
