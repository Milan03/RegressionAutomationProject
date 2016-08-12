using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class CataloguesPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        private IWebElement SearchInputTextbox;
        private IWebElement SearchButton;

        /// <summary>
        /// Page Object representing the view Catalogues page
        /// </summary>
        /// <param name="driver">Main interface for testing</param>
        public CataloguesPage(IWebDriver driver)
        {
            _driver = driver;
            Thread.Sleep(1000);
            SearchInputTextbox = HelperMethods.FindElement(driver, "id", "searchTerm");
            SearchButton = HelperMethods.FindElement(driver, "id", "catalogSearchButton");

            if (!Constants.Text.CAT_PAGE_TITLE.Equals(driver.Title))
            {
                _logger.Fatal(" > Member catalogue page navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            } else
                _logger.Info(" > Catalopue Selection page reached!");
        }

        /// <summary>
        /// Simulates inputting a catalogue to search for
        /// </summary>
        /// <param name="catalogue">name of catalogue</param>
        /// <returns>current page object</returns>
        public CataloguesPage InputCatalogueName(string catalogue)
        {
            _logger.Trace(" > Inputting catalogue name for search: " + catalogue);
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
            _logger.Trace(" > Searching for catalogue...");
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
            IWebElement catalogueTitle = HelperMethods.FindElement(_driver, "xpath", "//h1[contains(@class, 'catalog-tile-text') and contains (text(), '"+name+"')]");
            catalogueTitle.Click();
            Thread.Sleep(1000);
            return new ProductsPage(_driver);
        }

        /// <summary>
        /// Simulates choosing a catalogue for Drake members
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MyCartPage ChooseDrakeCatalogue(string name)
        {
            IWebElement catalogueTitle = HelperMethods.FindElement(_driver, "xpath", "//h1[contains(@class, 'catalog-tile-text') and contains (text(), '" + name + "')]");
            catalogueTitle.Click();
            Thread.Sleep(1000);
            return new MyCartPage(_driver, Constants.OrderType.NEW);
        }
    }
}
