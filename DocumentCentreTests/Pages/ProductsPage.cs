using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using DocumentCentreTests.Tables;
using DocumentCentreTests.Util;
using NLog;
using System.Threading;
using DocumentCentreTests.Catalogue;

namespace DocumentCentreTests.Pages
{
    public class ProductsPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;

        internal List<Product> _products;
        internal IList<IWebElement> _productTitles;
        internal IList<IWebElement> _productQtyUp;
        internal IList<IWebElement> _productQtyDown;
        internal IList<IWebElement> _productPrices;
        internal IList<IWebElement> _productUpdateBtns;

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

        #region Item Details Dialog
        private string ProductNumber;
        private string ListPrice;
        private IWebElement QuantityBox;
        private IWebElement QtyBoxUpArrow;
        private IWebElement QtyBoxDownArrow;
        private IWebElement UpdateCartButton;
        private IWebElement CancelButton;
        #endregion

        internal bool ItemAdded;

        public ProductsPage(IWebDriver driver)
        {
            #region Assigning Accessors
            this.Driver = driver;
            this.ProductsTable = HelperMethods.FindElement(Driver, "xpath", "//tbody");
            this.ReportsDropdown = HelperMethods.FindElement(Driver, "xpath", Constants.XPATH_REPORTS_LOCATOR);
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
                _logger.Fatal("       - Member's Products page not found.");
                throw new NoSuchWindowException("Member's Products page not found.");
            }
        }

        public MyCartPage NavigateToCart()
        {
            MyCartButton.Click();
            return new MyCartPage(Driver, "new_order");
        }

        public void LoadProductRows()
        {
            // get row elements
            this._productTitles = ProductsTable.FindElements(By.XPath(Constants.ROW_TITLE_XPATH));
            this._productQtyUp = ProductsTable.FindElements(By.XPath(Constants.ROW_QTY_UP_XPATH));
            this._productQtyDown = ProductsTable.FindElements(By.XPath(Constants.ROW_QTY_DOWN_XPATH));
            this._productPrices = ProductsTable.FindElements(By.XPath(Constants.ROW_PRICE_XPATH));
            this._productUpdateBtns = ProductsTable.FindElements(By.XPath(Constants.ROW_UPDATE_XPATH));

            // make product objects
            for (int i = 0; i < _productTitles.Count; ++i)
            {
                Product newProd = new Product();
                newProd.ProductTitle = _productTitles[i];
                newProd.Price = _productPrices[i];
                newProd.QtyUp = _productQtyUp[i];
                newProd.QtyDown = _productQtyDown[i];
                newProd.UpdateButton = _productUpdateBtns[i];
                _products.Add(newProd);
            }
        }

        public ProductsPage AddItemToCart(string prodName, int qty)
        {

            LoadProductRows();
            _products[0].SetQuantity(3);

            int x = 1;
            //var prodElement = HelperMethods.FindElement(Driver, "xpath", "//a[normalize-space(.) = '" + prodName + "']");
            // check alert
            //ItemAdded = HelperMethods.CheckItemAlert(Driver, newProd);
            return this;
        }

        public ProductsPage SwitchToTileView()
        {
            Thread.Sleep(1500);
            TilesViewButton.Click();
            Thread.Sleep(1000);
            return this;
        }

        public ProductsPage InputSearchTerm(string term)
        {
            _logger.Info("       - Inputting search term");
            SearchBar.Clear();
            SearchBar.SendKeys(term);
            return this;
        }

        public ProductsPage InitiateSearch()
        {
            _logger.Info("       - Searching for item...");
            SearchButton.Click();
            return this;
        }

        public ProductsPage OpenAdvancedSearch()
        {
            _logger.Info("       - Opening Advanced Search");
            AdvancedSearchLinktext.Click();
            return this;
        }

        public ProductsPage SaveDraftOrder()
        {
            SaveDraftButton.Click();
            return this;
        }

        public ProductsPage CataloguePDFExport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            DLCatAsPDFOption.Click();
            return this;
        }

        public ProductsPage CatalogueExcelExport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            DLCatAsExcelOption.Click();
            return this;
        }

        public ProductsPage GenerateOrderReport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            OrderReportOption.Click();
            return this;
        }

        public ProductsPage GenerateOrderSummary()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            SummaryOption.Click();
            return this;
        }

        public void LoadReportsOptions()
        {
            this.DLCatAsPDFOption = HelperMethods.FindElement(Driver, "id", "exportCatalogButtonPDF");
            this.DLCatAsExcelOption = HelperMethods.FindElement(Driver, "id", "exportCatalogButtonXLS");
            this.OrderReportOption = HelperMethods.FindElement(Driver, "id", "orderReportButton");
            this.SummaryOption = HelperMethods.FindElement(Driver, "id", "ordersSummaryButton");
        }

        public void LoadItemDetails()
        {
            this.ProductNumber = HelperMethods.FindElement(Driver, "xpath", "id('productDetailsTable')/tbody/tr[9]/td[2]/div").Text;
            this.ListPrice = HelperMethods.FindElement(Driver, "xpath", "id('productDetailsTable')/tbody/tr[5]/td[2]/div").Text;
            //this.QuantityBox = HelperMethods.FindElement(Driver, "xpath", "//div[contains(@title, '" + ProductNumber.Text + "')]/div/span/span/input[1]");
            this.QuantityBox = HelperMethods.FindElement(Driver, "xpath", "id('variant_18843667')/div[1]/span/span/input[1]");
            this.QtyBoxUpArrow = HelperMethods.FindElement(Driver, "xpath", "id('variant_18843667')/div[1]/span/span/span/span[1]/span");
            this.QtyBoxDownArrow = HelperMethods.FindElement(Driver, "xpath", "id('variant_18843667')/div[1]/span/span/span/span[2]");
            this.UpdateCartButton = HelperMethods.FindElement(Driver, "id", "updateOrderButton");
            this.CancelButton = HelperMethods.FindElement(Driver, "id", "cancelProductDialogButton");
            Thread.Sleep(2000);
        }
    }
}
