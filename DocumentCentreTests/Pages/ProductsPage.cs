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
        private IWebElement SearchBarLocator;
        private IWebElement SearchButtonLocator;
        private IWebElement AdvancedSearchLocator;
        private IWebElement GridButtonLocator;
        private IWebElement TileButtonLocator;
        private IWebElement ReportsDropdownLocator;
        private IWebElement SaveDraftLocator;

        #region Reports Dropdown Options
        private IWebElement DLCatAsPDFLocator;
        private IWebElement DLCatAsExcelLocator;
        private IWebElement OrderReportLocator;
        private IWebElement SummaryLocator;
        #endregion

        public ProductsPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.SearchBarLocator = HelperMethods.FindElement(driver, "id", "basicSearchTerm");
            this.SearchButtonLocator = HelperMethods.FindElement(driver, "id", "basicSearchButton");
            this.AdvancedSearchLocator = HelperMethods.FindElement(driver, "id", "advancedSearchLink");
            this.GridButtonLocator = HelperMethods.FindElement(driver, "id", "gridViewChoiec");
            this.TileButtonLocator = HelperMethods.FindElement(driver, "id", "tileViewChoice");
            this.SaveDraftLocator = HelperMethods.FindElement(driver, "id", "saveOrderButton");
            this.ReportsDropdownLocator = driver.FindElement(By.XPath(Constants.XPATH_REPORTS_LOCATOR));
            
            if (!driver.Title.Contains("Products")) 
            {
                _logger.Fatal("       - Member's Products page not found.");
                throw new NoSuchWindowException("Member's Products page not found.");
            }
        }

        public ProductsPage InputSearchTerm(string term)
        {
            _logger.Info("       - Inputting search term");
            SearchBarLocator.Clear();
            SearchBarLocator.SendKeys(term);
            return this;
        }

        public ProductsPage InitiateSearch()
        {
            _logger.Info("       - Searching for item...");
            SearchButtonLocator.Click();
            return this;
        }

        public ProductsPage OpenAdvancedSearch()
        {
            _logger.Info("       - Opening Advanced Search");
            AdvancedSearchLocator.Click();
            return this;
        }

        public ProductsPage SaveDraftOrder()
        {
            SaveDraftLocator.Click();
            return this;
        }

        public ProductsPage DownloadCatalogAsPDF()
        {
            ReportsDropdownLocator.Click();
            LoadDropdownOptions();
            DLCatAsPDFLocator.Click();
            return this;
        }

        public ProductsPage DownloadCatalogAsExcel()
        {
            ReportsDropdownLocator.Click();
            LoadDropdownOptions();
            DLCatAsExcelLocator.Click();
            return this;
        }

        public ProductsPage GenerateOrderReport()
        {
            ReportsDropdownLocator.Click();
            LoadDropdownOptions();
            OrderReportLocator.Click();
            return this;
        }

        public ProductsPage GenerateOrderSummary()
        {
            ReportsDropdownLocator.Click();
            LoadDropdownOptions();
            SummaryLocator.Click();
            return this;
        }

        public void LoadDropdownOptions()
        {
            this.DLCatAsPDFLocator = HelperMethods.FindElement(Driver, "id", "exportCatalogButtonPDF");
            this.DLCatAsExcelLocator = HelperMethods.FindElement(Driver, "id", "exportCatalogButtonXLS");
            this.OrderReportLocator = HelperMethods.FindElement(Driver, "id", "orderReportButton");
            this.SummaryLocator = HelperMethods.FindElement(Driver, "id", "ordersSummaryButton");
        }
    }
}
