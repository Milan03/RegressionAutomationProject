using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentCentreTests.Pages
{
    public class MyCartPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;

        #region UI Controls
        private IWebElement ReportsDropdown;
        private IWebElement DeleteOrderButton;
        private IWebElement SaveDraftButton;
        private IWebElement SendOrderButton;
        private IWebElement CloseOrderButton;
        private IWebElement ShipToDropdown;
        private IWebElement PONumberTextbox;
        private IWebElement POBuyerTextbox;
        private IWebElement UnitsTextbox;
        private IWebElement AmountTextbox;
        private IWebElement ShipOnTextbox;
        private IWebElement ShipOnButton;
        private IWebElement CancelAfterTextbox;
        private IWebElement CancelAfterButton;
        private IWebElement ContactNameTextbox;
        private IWebElement DeliveryAddressDisplay;
        private IWebElement DelieveryAddressButton;
        private IWebElement FreightTermsTextbox;
        private IWebElement PaymentTermsTextbox;
        private IWebElement NotesTextbox;
        #endregion

        #region Reports Dropdown Options
        private IWebElement ExportAsExcelOption;
        private IWebElement OrderReportOption;
        private IWebElement OrderSummaryOption;
        #endregion

        internal string AlertMessage; 

        public MyCartPage(IWebDriver driver, string type)
        {
            #region Assigning Accessors
            this.Driver = driver;
            this.ReportsDropdown = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_REPORTS_LOCATOR);

            if (type.Equals("newOrder"))
            {
                this.DeleteOrderButton = HelperMethods.FindElement(driver, "id", "deleteOrderButton");
                this.SaveDraftButton = HelperMethods.FindElement(driver, "id", "saveOrderButton");
                this.SendOrderButton = HelperMethods.FindElement(driver, "id", "completeOrderButton");
            }
            else
                this.CloseOrderButton = HelperMethods.FindElement(driver, "id", "closeOrderButton");

            this.ShipToDropdown = HelperMethods.FindElement(driver, "classname", "k-input");
            this.PONumberTextbox = HelperMethods.FindElement(driver, "id", "poNumber");
            this.POBuyerTextbox = HelperMethods.FindElement(driver, "id", "originalRefNumber");
            this.UnitsTextbox = HelperMethods.FindElement(driver, "id", "totalQuantityBox");
            this.AmountTextbox = HelperMethods.FindElement(driver, "id", "totalAmountBox");
            this.ShipOnTextbox = HelperMethods.FindElement(driver, "id", "requestedShipDate");
            this.ShipOnButton = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_SHIPON_CAL);
            this.CancelAfterTextbox = HelperMethods.FindElement(driver, "id", "cancelAfterDate");
            this.CancelAfterButton = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_CANCELAFTER_CAL);
            this.ContactNameTextbox = HelperMethods.FindElement(driver, "id", "contactName");
            this.DeliveryAddressDisplay = HelperMethods.FindElement(driver, "id", "addresseeName");
            this.DelieveryAddressButton = HelperMethods.FindElement(driver, "id", "changeAddressButton");
            this.FreightTermsTextbox = HelperMethods.FindElement(driver, "id", "frieghtTerms");
            this.PaymentTermsTextbox = HelperMethods.FindElement(driver, "id", "paymentTerms");
            this.NotesTextbox = HelperMethods.FindElement(driver, "id", "notes");
            #endregion

            if (!driver.Title.Contains("MyOrder"))
            {
                _logger.Fatal("       - Member's Cart page not found.");
                throw new NoSuchWindowException("Member's Cart page not found.");
            }
        }

        public void LoadReportsOptions()
        {
            this.ExportAsExcelOption = HelperMethods.FindElement(Driver, "id", "excelExportOrderButton");
            this.OrderReportOption = HelperMethods.FindElement(Driver, "id", "orderReportButton");
            this.OrderSummaryOption = HelperMethods.FindElement(Driver, "id", "orderSummaryButton");
        }

        public MyCartPage OrderExcelExport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            ExportAsExcelOption.Click();
            return this;
        }

        public MyCartPage GenerateOrderReport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            OrderReportOption.Click();
            return this;
        }

        public MyCartPage GenerateSummaryReport()
        {
            ReportsDropdown.Click();
            LoadReportsOptions();
            OrderSummaryOption.Click();
            return this;
        }

        public ViewOrdersPage DeleteOrder()
        {
            this.AlertMessage = "";
            DeleteOrderButton.Click();

            // click OK on Information dialog
            Thread.Sleep(500);
            Driver.FindElement(By.XPath(Constants.XPATH_INFO_OK)).Click();
            this.AlertMessage = HelperMethods.CheckAlert(Driver);
            return new ViewOrdersPage(Driver);
        }

        public MyCartPage SaveDraftOrder()
        {
            this.AlertMessage = "";

            // attempt to save
            SaveDraftButton.Click();
            Thread.Sleep(300);
            this.AlertMessage = HelperMethods.CheckAlert(Driver);
            if (AlertMessage.Equals(Constants.MISSING_INFO_MSG)) // if po missing
            {
                // enter a po and attempt to save again
                EnterRandomPONumber(7);
                SaveDraftButton.Click();
                this.AlertMessage = HelperMethods.CheckAlert(Driver);
            }
            return this;
        }

        public MyCartPage CompleteOrder()
        {
            this.AlertMessage = "";
            CloseOrderButton.Click();
            if (HelperMethods.IsElementPresent(Driver, By.ClassName("modal-content")))
            {
                // click OK on Information dialog
                Thread.Sleep(500);
                Driver.FindElement(By.XPath(Constants.XPATH_INFO_OK)).Click();
                this.AlertMessage = HelperMethods.CheckAlert(Driver);
                // click Finish on next dialog
                Thread.Sleep(500);
                HelperMethods.FindElement(Driver, "xpath", Constants.XPATH_INFO_FINISH).Click();
            }
            else
            {
                _logger.Error("       - Attempt to complete order [FAILED]");
                throw new ElementNotVisibleException("Could not detect Information modal.");
            }
            return this;
        }

        public void EnterPONumber(string po)
        {
            PONumberTextbox.SendKeys(po);
        }

        public void EnterRandomPONumber(int length)
        {
            PONumberTextbox.SendKeys(HelperMethods.RandomString(length));
        }
    }
}
