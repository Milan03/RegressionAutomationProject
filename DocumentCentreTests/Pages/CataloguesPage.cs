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

        public CataloguesPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.SearchInputLocator = HelperMethods.FindElement(driver, "id", "searchTerm");
            this.SearchButtonLocator = HelperMethods.FindElement(driver, "id", "catalogSearchButton");

            if (!Constants.CAT_PAGE_TITLE.Equals(driver.Title))
                throw new NoSuchWindowException("       - Member's Catalogue page not found.");
        }

        public CataloguesPage InputCatalogueName(string catalogue)
        {
            _logger.Info("      - Inputting catalogue name for search: " + catalogue);
            SearchInputLocator.Clear();
            SearchInputLocator.SendKeys(catalogue);
            return this;
        }

        public CataloguesPage SearchForCatalogue()
        {
            _logger.Info("       - Searching for catalogue...");
            SearchButtonLocator.Click();
            return this;
        }
    }
}
