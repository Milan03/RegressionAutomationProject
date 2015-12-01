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
        internal List<Product> myCart;

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
        private IWebElement ProductNumber;
        private IWebElement ListPrice;
        private IWebElement QuantityBox;
        private IWebElement UpdateCartButton;
        private IWebElement CancelButton;
        #endregion

        internal string AlertMessage;

        public ProductsPage(IWebDriver driver)
        {
            #region Assigning Accessors
            this.Driver = driver;
            this.ReportsDropdown = driver.FindElement(By.XPath(Constants.XPATH_REPORTS_LOCATOR));
            this.SaveDraftButton = HelperMethods.FindElement(driver, "id", "saveOrderButton");
            this.SearchBar = HelperMethods.FindElement(driver, "id", "basicSearchTerm");
            this.SearchButton = HelperMethods.FindElement(driver, "id", "basicSearchButton");
            this.AdvancedSearchLinktext = HelperMethods.FindElement(driver, "id", "advancedSearchLink");
            this.GridViewButton = HelperMethods.FindElement(driver, "id", "gridViewChoice");
            this.TilesViewButton = HelperMethods.FindElement(driver, "id", "tileViewChoice");
            this.MyCartButton = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_MYCART_LINK );
            this.myCart = new List<Product>();
            #endregion

            TilesViewButton.Click();

            if (!driver.Title.Contains("Products"))
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

        public ProductsPage AddItemToCart(string prodName, int qty)
        {
            // get and click product
            var prodElement = HelperMethods.FindElement(Driver, "xpath", "//h4[normalize-space(.) = '" + prodName + "']");
            prodElement.Click();
            // load product details
            Thread.Sleep(500);
            LoadItemDetails();
            // enter quantity
            QuantityBox.Clear();
            QuantityBox.SendKeys(qty.ToString());
            // add item to cart
            Product newProd = new Product();
            newProd.SupplierProductNumber = ProductNumber.Text;
            newProd.Description = prodName;
            newProd.Price = Decimal.Parse(ListPrice.Text);
            newProd.Quantity = qty;
            newProd.AmountTotal = newProd.Price * qty;
            myCart.Add(newProd);
            UpdateCartButton.Click();
            // check alert in test
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
            this.ProductNumber = HelperMethods.FindElement(Driver, "xpath", "id('productDetailsTable')/tbody/tr[9]/td[2]/div");
            this.ListPrice = HelperMethods.FindElement(Driver, "xpath", "id('productDetailsTable')/tbody/tr[5]/td[2]/div");
            this.QuantityBox = HelperMethods.FindElement(Driver, "xpath", "//input[contains(@class, 'qty-box')]");
            this.UpdateCartButton = HelperMethods.FindElement(Driver, "id", "updateOrderButton");
            this.CancelButton = HelperMethods.FindElement(Driver, "id", "cancelProductDialogButton");
        }
    }
}
