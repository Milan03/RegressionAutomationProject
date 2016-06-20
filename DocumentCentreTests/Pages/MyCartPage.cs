using DocumentCentreTests.Catalogue;
using DocumentCentreTests.Util;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DocumentCentreTests.Pages
{
    public class MyCartPage
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        internal List<CartItem> _cartLineItems;
        internal IList<IWebElement> _itemDeleteButtons;
        internal IList<IWebElement> _itemProdNums;
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
        internal bool OrderComplete;
        internal bool AlertSuccess;

        public MyCartPage(IWebDriver driver, string type)
        {
            #region Assigning Accessors
            OrderComplete = false;
            _driver = driver;
            _cartLineItems = new List<CartItem>();
            _itemDeleteButtons = new List<IWebElement>();
            _itemProdNums = new List<IWebElement>();
            _itemPrices = new List<IWebElement>();
            _itemQtys = new List<IWebElement>();
            _itemTotals = new List<IWebElement>();
            ItemDeleted = false;
            ReportsDropdown = HelperMethods.FindElement(_driver, "xpath", Constants.REPORTS_LOCATOR_XP);

            if (type.Equals("new_order"))
            {
                OrderComplete = false;
                DeleteOrderButton = HelperMethods.FindElement(_driver, "id", "deleteOrderButton");
                SaveDraftButton = HelperMethods.FindElement(_driver, "id", "saveOrderButton");
                SendOrderButton = HelperMethods.FindElement(_driver, "id", "completeOrderButton");
                DelieveryAddressButton = HelperMethods.FindElement(_driver, "id", "changeAddressButton");
                _logger.Info(" > MyCart page reached!");
            }
            else if (type.Equals("order_complete"))
            {
                CloseOrderButton = HelperMethods.FindElement(_driver, "id", "closeOrderButton");
                OrderComplete = true;
                _logger.Info(" > Purchase Order completed!");
            }
            CartTable = HelperMethods.FindElement(_driver, "xpath", "//tbody");
            ShipToDropdown = HelperMethods.FindElement(_driver, "classname", "k-input");
            PONumberTextbox = HelperMethods.FindElement(_driver, "id", "poNumber");
            POBuyerTextbox = HelperMethods.FindElement(_driver, "id", "originalRefNumber");
            UnitsTextbox = HelperMethods.FindElement(_driver, "id", "totalQuantityBox");
            AmountTextbox = HelperMethods.FindElement(_driver, "id", "totalAmountBox");
            ContactNameTextbox = HelperMethods.FindElement(_driver, "id", "contactName");
            DeliveryAddressDisplay = HelperMethods.FindElement(_driver, "id", "addresseeName");
            FreightTermsTextbox = HelperMethods.FindElement(_driver, "id", "freightTerms");
            PaymentTermsTextbox = HelperMethods.FindElement(_driver, "id", "paymentTerms");
            NotesTextbox = HelperMethods.FindElement(_driver, "id", "notes");
            #endregion

            if (!_driver.Url.Contains("MyOrder") && !OrderComplete)
            {
                _logger.Fatal(" > MyCart navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
        }

        /// <summary>
        /// Loads all the items currently in the cart and makes objects out of the 
        /// row information to interact with. Each grid column is seperated into 
        /// lists and then the lists are looped through to apply appropriate row 
        /// elements per object constructed.
        /// </summary>
        internal void LoadItemsInCart()
        {
            _logger.Trace(" > Attempting to load cart items...");
            try
            {
                // get row elements
                _itemDeleteButtons = CartTable.FindElements(By.XPath(Constants.ITEM_DEL_BTN_XP));
                _itemProdNums = CartTable.FindElements(By.XPath(Constants.ITEM_PN_XP));
                _itemPrices = CartTable.FindElements(By.XPath(Constants.ITEM_PRICE_XP));
                _itemQtys = CartTable.FindElements(By.XPath(Constants.ITEM_QTY_XP));
                _itemTotals = CartTable.FindElements(By.XPath(Constants.ITEM_TOTAL_XP));
                // make cart item objects to work with
                for (int i = 0; i < _itemProdNums.Count - 1; ++i)
                {
                    CartItem item = new CartItem();
                    item.DeleteButton = _itemDeleteButtons[i];
                    item.ProductNumber = _itemProdNums[i];
                    item.Price = _itemPrices[i];
                    item.Quantity = _itemQtys[i];
                    item.ItemTotalAmt = _itemTotals[i];
                    item.Checked = false;
                    _cartLineItems.Add(item);
                }
                if (!_cartLineItems.Any())
                {
                    CartItem item = new CartItem();
                    item.DeleteButton = _itemDeleteButtons.First();
                    item.ProductNumber = _itemProdNums.First();
                    item.Price = _itemPrices.First();
                    item.Quantity = _itemQtys.First();
                    item.ItemTotalAmt = _itemTotals.First();
                    item.Checked = false;
                    _cartLineItems.Add(item);
                }
                _logger.Info(" > Cart items loaded!");
            }
            catch (Exception)
            {
                _logger.Fatal(" > Loading MyCart items [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
        }

        /// <summary>
        /// Find specific item in user's cart. 
        /// If a match is found using Product Numbers, a CartItem object is build and
        /// returned to the caller.
        /// </summary>
        /// <param name="prodNum">description of item to be searched for</param>
        /// <returns>Cart object to interact with</returns>
        private CartItem LoadCartItem(string prodNum)
        {
            _logger.Trace(" > Attempting to find cart item: " + prodNum);
            CartItem currentItem = new CartItem();
            try
            {
                for (int i = 0; i < _cartLineItems.Count; ++i)
                {
                    // search current cart items; if match make temp obj and return
                    if (_cartLineItems[i].ProductNumber.Text.Equals(prodNum))
                    {
                        currentItem.DeleteButton = _itemDeleteButtons[i];
                        currentItem.ProductNumber = _itemProdNums[i];
                        currentItem.Price = _itemPrices[i];
                        currentItem.Quantity = _itemQtys[i];
                        currentItem.ItemTotalAmt = _itemTotals[i];
                        _logger.Info(" > Cart item found: " + prodNum);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Fatal(" > Attempting to find MyCart item [FAILED] - " + e.Message);
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            return currentItem;
        }

        internal MyCartPage AddItemInline(List<Product> prodsInCart, string pnToAdd, int qtyToAdd)
        {
            Thread.Sleep(1000);
            IWebElement PNCell, QtyInput, Outside, Active;
            //PNInput = null;
            PNCell = _driver.FindElement(By.XPath("//td[@class='editable']"));
            QtyInput = HelperMethods.FindElement(_driver, "xpath", "//td[contains(@class, 'editable') and contains(@class,'grid-col-int')]");
            Outside = HelperMethods.FindElement(_driver, "id", "mainOrderScreenTabs-2");
            Actions action = new Actions(_driver);
            action.MoveToElement(PNCell).Click().SendKeys("IIN-MILANTEST-01");
            //Thread.Sleep(1000);
            action.Perform();
            Outside.Click();
            
            for (int i = 0; i < qtyToAdd; ++i)
            {
                Active = HelperMethods.FindElement(_driver, "xpath", "//td[contains(@class,'editable')]//span[contains(@class,'k-i-arrow-n')]");
                action.MoveToElement(Active).Click().Click().Click();
                action.Perform();
            }
            return this;
        }

        /// <summary>
        /// Compares values of items in cart vs values of items recorded while on the Products page.
        /// Adds 1 to 'consistencyCount' if all values of an item match those of cart item. If the 
        /// count matches the prodsInCart.Count then the verification is successful.
        /// </summary>
        /// <param name="prodsInCart">List of products added at the Product page</param>
        /// <returns>True if matching, false if not matching</returns>
        internal bool VerifyItemsInCart(List<Product> prodsInCart)
        {
            int consistencyCount = 0;
            if (!prodsInCart.Count.Equals(_cartLineItems.Count))
                return false;
            else
            {
                _logger.Trace(" > Verifying items added vs. items in cart...");
            restart:
                foreach (CartItem cartLineItem in _cartLineItems)
                {
                    foreach (Product prod in prodsInCart)
                    {
                        if (prod.ProductNumber.Equals(cartLineItem.ProductNumber.Text) &&
                            !prod.Checked && !cartLineItem.Checked)
                        {
                            int cartItemQty;
                            Int32.TryParse(cartLineItem.Quantity.Text, out cartItemQty);
                            if (prod.Price.Equals(cartLineItem.Price.Text) &&
                                prod.Quantity.Equals(cartItemQty))
                            {
                                prod.Checked = true;
                                cartLineItem.Checked = true;
                                double prodPrice = 0;
                                double cartLineItemTotal = 0;
                                Double.TryParse(prod.Price, out prodPrice);
                                Double.TryParse(cartLineItem.ItemTotalAmt.Text, out cartLineItemTotal);
                                double prodTotal = prodPrice * prod.Quantity;
                                if (prodTotal.Equals(cartLineItemTotal))
                                    consistencyCount++;
                                goto restart;
                            }
                        }
                        else
                            continue;
                    }
                }
                if (consistencyCount.Equals(prodsInCart.Count))
                {
                    _logger.Info(" > Verification SUCCESS - all items match!");
                    return true;
                }
                else
                {
                    _logger.Info(" > Verification FAILURE!");
                    return false;
                }
            }
        }

        /// <summary>
        /// Simulate the deletion of an item from the cart. Loop through the constructed
        /// _cartLineItems list, if any Product Numbers match then click that row's 
        /// delete icon and confirm the delete.
        /// </summary>
        /// <param name="pn">Find item by product number</param>
        /// <returns>Current page object</returns>
        public MyCartPage RemoveItemFromCart(string pn)
        {
            Thread.Sleep(1000);
            CartItem item;
            try
            {
                _logger.Trace(" > Attempting to delete a cart item...");
                if (_cartLineItems.Any())
                {
                    foreach (CartItem cItem in _cartLineItems)
                    {
                        if (cItem.ProductNumber.Text.Equals(pn))
                        {
                            ItemDeleted = false;
                            item = LoadCartItem(cItem.ProductNumber.Text);
                            item.DeleteButton.Click();

                            Thread.Sleep(1000);
                            _driver.FindElement(By.XPath(Constants.DEL_ITEM_OK_XP)).Click();

                            // check alert for confirmation of delete
                            ItemDeleted = HelperMethods.CheckAlert(_driver);

                            if (ItemDeleted)
                            {
                                _logger.Info(" > Cart item deleted! Item: " + pn);
                                break;
                            }
                        }
                    }
                }
                else
                    _logger.Error(" > No items in cart!");
            }
            catch (StaleElementReferenceException)
            {
                _logger.Info("Stale element detected. Ignoring...");
            }
            catch (Exception e)
            {
                _logger.Fatal(" > Removing item from MyCart [FAILED] - " + e.Message);
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            return new MyCartPage(_driver, "new_order");
            //return this;
        }

        /// <summary>
        /// Simulate the deletion of an order. 
        /// </summary>
        /// <returns>View Orders page object</returns>
        public MemberHomePage DeleteOrder()
        {
            IWebElement OK;
            DeleteOrderButton.Click();
            // click OK on Information dialog
            Thread.Sleep(500);
            OK = _driver.FindElement(By.XPath(Constants.ORDER_OK_XP));
            OK.Click();
            return new MemberHomePage(_driver);
        }

        /// <summary>
        /// Saves a purhcase order as 'Draft'.
        /// </summary>
        /// <returns>current page element</returns>
        public MyCartPage SaveDraftOrder()
        {
            AlertSuccess = false;

            _logger.Trace(" > Attempting to save draft order...");
            SaveDraftButton.Click();
            Thread.Sleep(300);
            AlertSuccess = HelperMethods.CheckAlert(_driver);
            if (AlertSuccess.Equals(Constants.MISSING_INFO_MSG)) // if po missing
            {
                // enter a po and attempt to save again
                EnterRandomPONumber(7);
                SaveDraftButton.Click();
                AlertSuccess = HelperMethods.CheckAlert(_driver);
            }
            return this;
        }

        /// <summary>
        /// Completes the Send order process.
        /// </summary>
        /// <returns>Current page object</returns>
        public MyCartPage SendOrder()
        {
            AlertSuccess = false;
            _logger.Trace(" > Attempting to send order...");
            SendOrderButton.Click();
            if (HelperMethods.IsElementPresent(_driver, By.ClassName("modal-content")))
            {
                // click OK on Information dialog
                Thread.Sleep(500);
                HelperMethods.FindElement(_driver, "xpath", Constants.ORDER_OK_XP).Click();
                // click Finish on next dialog
                Thread.Sleep(5000);
                HelperMethods.FindElement(_driver, "xpath", Constants.INFO_FINISH_XP).Click();
                OrderComplete = true;
                return new MyCartPage(_driver, "order_complete");
            }
            else
            {
                _logger.Fatal(" > Completing order [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + _driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
                return this;
            }
        }

        public ViewOrdersPage CompleteOrder()
        {
            Thread.Sleep(1000);
            CloseOrderButton.Click();
            return new ViewOrdersPage(_driver);
        }

        public void LoadReportsOptions()
        {
            ExportAsExcelOption = HelperMethods.FindElement(_driver, "id", "excelExportOrderButton");
            OrderReportOption = HelperMethods.FindElement(_driver, "id", "orderReportButton");
            OrderSummaryOption = HelperMethods.FindElement(_driver, "id", "orderSummaryButton");
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
