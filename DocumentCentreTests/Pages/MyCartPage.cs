﻿using DocumentCentreTests.Catalogue;
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

        internal IList<CartItem> _cartItems;
        internal IList<IWebElement> _itemDeleteButtons;
        internal IList<IWebElement> _itemTitles;
        internal IList<IWebElement> _itemDescriptions;
        internal IList<IWebElement> _itemPrices;
        internal IList<IWebElement> _itemQtys;
        internal IList<IWebElement> _itemTotals;

        #region UI Controls
        private IWebElement CartTable;
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

        internal bool ItemDeleted;
        internal bool AlertSuccess;

        public MyCartPage(IWebDriver driver, string type)
        {
            #region Assigning Accessors
            bool dontCheck = false;
            this.Driver = driver;
            this._cartItems = new List<CartItem>();
            this.ItemDeleted = false;
            this.ReportsDropdown = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_REPORTS_LOCATOR);

            if (type.Equals("new_order"))
            {
                dontCheck = false;
                this.DeleteOrderButton = HelperMethods.FindElement(driver, "id", "deleteOrderButton");
                this.SaveDraftButton = HelperMethods.FindElement(driver, "id", "saveOrderButton");
                this.SendOrderButton = HelperMethods.FindElement(driver, "id", "completeOrderButton");
            }
            else if (type.Equals("order_complete"))
            {
                this.CloseOrderButton = HelperMethods.FindElement(driver, "id", "closeOrderButton");
                dontCheck = true;
            }
            this.CartTable = HelperMethods.FindElement(driver, "xpath", "//tbody");
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
            this.FreightTermsTextbox = HelperMethods.FindElement(driver, "id", "freightTerms");
            this.PaymentTermsTextbox = HelperMethods.FindElement(driver, "id", "paymentTerms");
            this.NotesTextbox = HelperMethods.FindElement(driver, "id", "notes");
            #endregion

            if (!driver.Url.Contains("MyOrder") && !dontCheck)
            {
                _logger.Fatal("      - ERROR: Member's Cart page not found.");
            }
        }

        /// <summary>
        /// Loads all the items currently in the cart and makes objects out of the 
        /// row information to interact with.
        /// </summary>
        private void LoadItemsInCart()
        {
            _logger.Info("       - Attempting to load cart items...");

            // get row elements
            this._itemDeleteButtons = CartTable.FindElements(By.XPath(Constants.ITEM_DEL_BTN_XP));
            this._itemTitles = CartTable.FindElements(By.XPath(Constants.ITEM_TITLE_XP));
            this._itemDescriptions = CartTable.FindElements(By.XPath(Constants.ITEM_DES_XP));
            this._itemPrices = CartTable.FindElements(By.XPath(Constants.ITEM_PRICE_XP));
            this._itemQtys = CartTable.FindElements(By.XPath(Constants.ITEM_QTY_XP));
            this._itemTotals = CartTable.FindElements(By.XPath(Constants.ITEM_TOTAL_XP));

            // make cart item objects to work with
            for (int i = 0; i < _itemTitles.Count - 1; ++i)
            {
                CartItem item = new CartItem();
                item.DeleteButton = _itemDeleteButtons[i];
                item.ProductTitle = _itemTitles[i];
                item.Description = _itemDescriptions[i];
                item.Price = _itemPrices[i];
                item.Quantity = _itemQtys[i];
                item.ItemTotalAmt = _itemTotals[i];
                _cartItems.Add(item);
            }
        }

        /// <summary>
        /// Find specific item in user's cart
        /// </summary>
        /// <param name="itemDes">description of item to be searched for</param>
        /// <returns>Cart object to interact with</returns>
        private CartItem LoadCartItem(string itemDes)
        {
            _logger.Info("       - Attempting to find cart item: " +itemDes);
            CartItem currentItem = new CartItem();
            for (int i = 0; i < _cartItems.Count; ++i)
            {
                if (_itemDescriptions[i].Text.Equals(itemDes))
                {
                    currentItem.DeleteButton = _itemDeleteButtons[i];
                    currentItem.ProductTitle = _itemTitles[i];
                    currentItem.Description = _itemDescriptions[i];
                    currentItem.Price = _itemPrices[i];
                    currentItem.Quantity = _itemQtys[i];
                    currentItem.ItemTotalAmt = _itemTotals[i];
                }
            }
            return currentItem;
        }

        /// <summary>
        /// Simulate the deletion of an item from the cart
        /// </summary>
        /// <param name="itemDes">How to find the item</param>
        /// <returns>Current page object</returns>
        public MyCartPage RemoveItemFromCart()
        {
            Thread.Sleep(1000);
            CartItem item;
            // find item 
            LoadItemsInCart();
            if (_cartItems.Any())
            {
                item = LoadCartItem(_cartItems.First().Description.Text);
                item.DeleteButton.Click();

                // click OK on Information dialog
                Thread.Sleep(1000);
                Driver.FindElement(By.XPath(Constants.XPATH_DEL_ITEM_OK)).Click();

                // check alert for confirmation of delete
                this.ItemDeleted = HelperMethods.CheckAlert(Driver);
                return this;
            }
            else
            {
                this.ItemDeleted = false;
                _logger.Error("      - No items in cart!");
                return this;
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

        /// <summary>
        /// Simulate the deletion of an order
        /// </summary>
        /// <returns></returns>
        public ViewOrdersPage DeleteOrder()
        {
            this.AlertSuccess = false;
            DeleteOrderButton.Click();

            // click OK on Information dialog
            Thread.Sleep(500);
            Driver.FindElement(By.XPath(Constants.XPATH_INFO_OK)).Click();
            this.AlertSuccess = HelperMethods.CheckAlert(Driver);
            return new ViewOrdersPage(Driver);
        }

        public MyCartPage SaveDraftOrder()
        {
            this.AlertSuccess = false;

            // attempt to save
            SaveDraftButton.Click();
            Thread.Sleep(300);
            this.AlertSuccess = HelperMethods.CheckAlert(Driver);
            if (AlertSuccess.Equals(Constants.MISSING_INFO_MSG)) // if po missing
            {
                // enter a po and attempt to save again
                EnterRandomPONumber(7);
                SaveDraftButton.Click();
                this.AlertSuccess = HelperMethods.CheckAlert(Driver);
            }
            return this;
        }

        public MyCartPage CompleteOrder()
        {
            this.AlertSuccess = false;
            CloseOrderButton.Click();
            if (HelperMethods.IsElementPresent(Driver, By.ClassName("modal-content")))
            {
                // click OK on Information dialog
                Thread.Sleep(500);
                Driver.FindElement(By.XPath(Constants.XPATH_INFO_OK)).Click();
                this.AlertSuccess = HelperMethods.CheckAlert(Driver);
                // click Finish on next dialog
                Thread.Sleep(500);
                HelperMethods.FindElement(Driver, "xpath", Constants.XPATH_INFO_FINISH).Click();
                return new MyCartPage(Driver, "order_complete");
            }
            else
            {
                _logger.Error("       - Attempt to complete order [FAILED]");
                throw new ElementNotVisibleException("Could not detect Information modal.");
            }
        }

        public MyCartPage EnterPONumber(string po)
        {
            PONumberTextbox.Clear();
            PONumberTextbox.SendKeys(po);
            return this;
        }

        public MyCartPage EnterRandomPONumber(int length)
        {
            PONumberTextbox.Clear();
            PONumberTextbox.SendKeys(HelperMethods.RandomString(length));
            return this;
        }

        public MyCartPage EnterBuyerPO(string po)
        {
            POBuyerTextbox.Clear();
            POBuyerTextbox.SendKeys(po);
            return this;
        }

        public MyCartPage EnterShipOnDate(int year, int mo, int day) 
        {
            ShipOnTextbox.Clear();
            ShipOnTextbox.SendKeys(year + "-" + mo + "-" + day);
            return this;
        }

        public MyCartPage EnterCancelAfterDate(int year, int mo, int day)
        {
            ShipOnTextbox.Clear();
            CancelAfterTextbox.SendKeys(year + "-" + mo + "-" + day);
            return this;
        }

        public MyCartPage EnterContactName(string contact)
        {
            ContactNameTextbox.Clear();
            ContactNameTextbox.SendKeys(contact);
            return this;
        }

        public MyCartPage ChangeDeliveryAddress() 
        {
            DelieveryAddressButton.Click();
            // TODO: modal logic
            return this;
        }

        public MyCartPage EnterFreightTerms(string terms)
        {
            FreightTermsTextbox.Clear();
            FreightTermsTextbox.SendKeys(terms);
            return this;
        }

        public MyCartPage EnterPaymentTerms(string terms)
        {
            PaymentTermsTextbox.Clear();
            PaymentTermsTextbox.SendKeys(terms);
            return this;
        }

        public MyCartPage EnterNotes(string notes)
        {
            NotesTextbox.Clear();
            NotesTextbox.SendKeys(notes);
            return this;
        }
    }
}
