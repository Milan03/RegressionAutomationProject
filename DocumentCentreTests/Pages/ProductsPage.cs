using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using DocumentCentreTests.Tables;
using DocumentCentreTests.Util;
using NLog;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class ProductsPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;

        private IWebElement ReportsDropdown;
        private IWebElement SaveDraftButton;
        private IWebElement SearchBar;
        private IWebElement SearchButton;
        private IWebElement AdvancedSearchLinktext;
        private IWebElement GridViewButton;
        private IWebElement TileButton;
        private IWebElement MyCartButton;
        
        #region Reports Dropdown Options
        private IWebElement DLCatAsPDFOption;
        private IWebElement DLCatAsExcelOption;
        private IWebElement OrderReportOption;
        private IWebElement SummaryOption;
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
            this.TileButton = HelperMethods.FindElement(driver, "id", "tileViewChoice");
            this.MyCartButton = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_MYCART_LINK );
            #endregion

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
    }
}
