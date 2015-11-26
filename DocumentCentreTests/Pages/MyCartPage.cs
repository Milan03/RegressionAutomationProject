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
    public class MyCartPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        private IWebDriver Driver;

        #region UI Controls
        private IWebElement ReportsDropdown;
        private IWebElement DeleteOrderButton;
        private IWebElement SaveDraftButton;
        private IWebElement SendOrderButton;
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
        private IWebElement ExportOption;
        private IWebElement OrderReportOption;
        private IWebElement OrderSummaryOption;
        #endregion

        public MyCartPage(IWebDriver driver)
        {
            this.Driver = driver;
            this.ReportsDropdown = driver.FindElement(By.XPath(Constants.XPATH_REPORTS_LOCATOR));
            this.DeleteOrderButton = HelperMethods.FindElement(driver, "id", "deleteOrderButton");
            this.SaveDraftButton = HelperMethods.FindElement(driver, "id", "saveOrderButton");
            this.SendOrderButton = HelperMethods.FindElement(driver, "id", "completeOrderButton");
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

            if (!driver.Title.Contains("MyOrder"))
            {
                _logger.Fatal("       - Member's Cart page not found.");
                throw new NoSuchWindowException("Member's Cart page not found.");
            }
        }
    }
}
