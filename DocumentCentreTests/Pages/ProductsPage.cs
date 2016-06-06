using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using DocumentCentreTests.Util;
using NLog;
using System.Threading;
using DocumentCentreTests.Catalogue;

namespace DocumentCentreTests.Pages
{
    public class ProductsPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver driver;

        // product data structures
        internal List<Product> _products;
        internal IList<IWebElement> _productRowWrappers;
        internal IList<IWebElement> _productVariants;
        internal IList<IWebElement> _productTitles;
        internal IList<IWebElement> _productQtyUp;
        internal IList<IWebElement> _productQtyDown;
        internal IList<IWebElement> _productPrices;
        internal IList<IWebElement> _productUpdateBtns;

        // physical elements (locators) on page
        private IWebElement ProductsTable;
        private IWebElement ReportsDropdown;
        private IWebElement SaveDraftButton;
        private IWebElement SearchBar;
        private IWebElement SearchButton;
        private IWebElement AdvancedSearchLinktext;
        private IWebElement GridViewButton;
        private IWebElement TilesViewButton;
        private IWebElement MyCartButton;
        
        #region Reports Dropdown Options
        private IWebElement DLCatAsPDFOption;
        private IWebElement DLCatAsExcelOption;
        private IWebElement OrderReportOption;
        private IWebElement SummaryOption;
        #endregion

        internal bool ItemAdded;

        public ProductsPage(IWebDriver driver)
        {
            #region Assigning Accessors
            this.driver = driver;
            this.ItemAdded = false;
            this.ProductsTable = HelperMethods.FindElement(this.driver, "xpath", "//tbody");
            this.ReportsDropdown = HelperMethods.FindElement(this.driver, "xpath", Constants.XPATH_REPORTS_LOCATOR);
            this.SaveDraftButton = HelperMethods.FindElement(driver, "id", "saveOrderButton");
            this.SearchBar = HelperMethods.FindElement(driver, "id", "basicSearchTerm");
            this.SearchButton = HelperMethods.FindElement(driver, "id", "basicSearchButton");
            this.AdvancedSearchLinktext = HelperMethods.FindElement(driver, "id", "advancedSearchLink");
            this.GridViewButton = HelperMethods.FindElement(driver, "id", "gridViewChoice");
            this.TilesViewButton = HelperMethods.FindElement(driver, "id", "tileViewChoice");
            this.MyCartButton = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_MYCART_LINK );
            this._products = new List<Product>();
            #endregion

            if (!driver.Url.Contains("Products"))
            {
                _logger.Fatal("       - ERROR: Member's Products page not found.");
            }
        }

        /// <summary>
        /// Loads all physical elements (locators) of Reports dropdown
        /// </summary>
        public void LoadReportsOptions()
        {
            this.DLCatAsPDFOption = HelperMethods.FindElement(driver, "id", "exportCatalogButtonPDF");
            this.DLCatAsExcelOption = HelperMethods.FindElement(driver, "id", "exportCatalogButtonXLS");
            this.OrderReportOption = HelperMethods.FindElement(driver, "id", "orderReportButton");
            this.SummaryOption = HelperMethods.FindElement(driver, "id", "ordersSummaryButton");
        }

        /// <summary>
        /// Loads all products on current catalogue page using row elements
        /// </summary>
        private void LoadProductRows()
        {
            _logger.Info("       - Attempting to load catalogue products...");

            this._productVariants = ProductsTable.FindElements(By.CssSelector(Constants.ALL_PROD_VARIANTS));
            this._productQtyUp = ProductsTable.FindElements(By.XPath(Constants.ROW_QTY_UP_XPATH));
            this._productQtyDown = ProductsTable.FindElements(By.XPath(Constants.ROW_QTY_DOWN_XPATH));

            for (int i = 0; i < _productVariants.Count; ++i)
            {
                string prodInfo = "start" + _productVariants[i].GetAttribute("title") + "end";
                string varInfo = _productVariants[i].Text + "end";
                Product newProd = new Product();
                newProd.ProductNumber = HelperMethods.GetBetween(prodInfo, "Product #: ", " | ");
                newProd.UPC = HelperMethods.GetBetween(prodInfo, "UPC: ", "end");
                newProd.Colour = HelperMethods.GetBetween(varInfo, "Color: ", "\r\n");
                newProd.Size = HelperMethods.GetBetween(varInfo, "Size: ", "\r\n");
                newProd.Price = HelperMethods.GetBetween(varInfo, "$ ", "end");
                newProd.QtyUp = _productQtyUp[i];
                newProd.QtyDown = _productQtyDown[i];
                //newProd.UpdateButton = _productUpdateBtns[i];
                _products.Add(newProd);
            }

        }

        /// <summary>
        /// Find specific product in catalogue page to interat with
        /// </summary>
        /// <param name="prodNum">name of product to be searched for</param>
        /// <returns>Product object to interact with</returns>
        private Product LoadProduct(string prodNum)
        {
            _logger.Info("       - Searching for product: " +prodNum);
            Product currentProd = new Product();
            for (int i = 0; i < _products.Count; ++i)
            {
                if (_products[i].ProductNumber.Equals(prodNum))
                {

                }
            }
            return currentProd;
        }

        /// <summary>
        /// Simulates the addition of an item to the cart and checks if the alert popup
        /// is accurate.
        /// </summary>
        /// <param name="prodName">name of product to add to cart</param>
        /// <param name="qty">quantity of product to add to cart</param>
        /// <returns>current page object</returns>
        public ProductsPage AddItemToCart(string prodName, int qty)
        {
            _logger.Info("       - Attempting to add [" +qty +"] '" +prodName +"' to cart.");
            WaitForLoad();
            Thread.Sleep(1000);
            // find product
            LoadProductRows();
            Product product = LoadProduct(prodName);
            _logger.Info("       - Setting product quantity...");
            product.SetQuantity(qty);
            _logger.Info("       - Adding item to cart...");
            product.UpdateButton.Click();
            this.ItemAdded = HelperMethods.CheckItemAddAlert(driver, product);
            return this;
        }

        /// <summary>
        /// Simulates navigation to cart
        /// </summary>
        /// <returns>new MyCart page object</returns>
        public MyCartPage NavigateToCart()
        {
            _logger.Info("       - Attempting to navigate to cart...");
            try {
                Thread.Sleep(1000);
                MyCartButton.Click();
                Thread.Sleep(2000);
            } catch (NoSuchWindowException e)
            {
                _logger.Fatal("         - Navigation to cart [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            return new MyCartPage(driver, "new_order");
        }

        public ProductsPage WaitForLoad()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => !driver.FindElement(By.Id("waitMessage")).Displayed);
            return this;
        }

        /// <summary>
        /// Simulates switching to the Tile products view of a catalogue
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage SwitchToTileView()
        {
            Thread.Sleep(1500);
            TilesViewButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        /// <summary>
        /// Simulates searching for a product
        /// </summary>
        /// <param name="term">term to search for</param>
        /// <returns>current page object</returns>
        public ProductsPage InputSearchTerm(string term)
        {
            _logger.Info("       - Inputting search term:" +term);
            SearchBar.Clear();
            SearchBar.SendKeys(term);
            return this;
        }

        /// <summary>
        /// Simulates click the search button for product search
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage InitiateSearch()
        {
            _logger.Info("       - Searching for item...");
            SearchButton.Click();
            return this;
        }

        /// <summary>
        /// Simulates opening the advanced search options partial form
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage OpenAdvancedSearch()
        {
            _logger.Info("       - Opening Advanced Search");
            AdvancedSearchLinktext.Click();
            return this;
        }

        /// <summary>
        /// Simulates saving the current draft order
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage SaveDraftOrder()
        {
            SaveDraftButton.Click();
            return this;
        }

        /// <summary>
        /// Simulates clicking catalogue pdf export from Reports dropdown
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage CataloguePDFExport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            DLCatAsPDFOption.Click();
            return this;
        }

        /// <summary>
        /// Simulates clicking catalgoue excel export from Reports dropdown
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage CatalogueExcelExport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            DLCatAsExcelOption.Click();
            return this;
        }

        /// <summary>
        /// Simulates generate order report from Reports dropdown
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage GenerateOrderReport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            OrderReportOption.Click();
            return this;
        }

        /// <summary>
        /// Simulates generating an order summary from Reports dropdown
        /// </summary>
        /// <returns>current page object</returns>
        public ProductsPage GenerateOrderSummary()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            SummaryOption.Click();
            return this;
        }


    }
}
