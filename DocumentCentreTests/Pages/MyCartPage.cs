using DocumentCentreTests.Catalogue;
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
        private IWebDriver driver;

        internal List<CartItem> _cartLineItems;
        internal IList<IWebElement> _itemDeleteButtons;
        internal IList<IWebElement> _itemProdNums;
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
            this.OrderComplete = false;
            this.driver = driver;
            this._cartLineItems = new List<CartItem>();
            this.ItemDeleted = false;
            this.ReportsDropdown = HelperMethods.FindElement(driver, "xpath", Constants.XPATH_REPORTS_LOCATOR);

            if (type.Equals("new_order"))
            {
                OrderComplete = false;
                this.DeleteOrderButton = HelperMethods.FindElement(driver, "id", "deleteOrderButton");
                this.SaveDraftButton = HelperMethods.FindElement(driver, "id", "saveOrderButton");
                this.SendOrderButton = HelperMethods.FindElement(driver, "id", "completeOrderButton");
                this.DelieveryAddressButton = HelperMethods.FindElement(driver, "id", "changeAddressButton");
                _logger.Info(" > MyCart page reached!");
            }
            else if (type.Equals("order_complete"))
            {
                this.CloseOrderButton = HelperMethods.FindElement(driver, "id", "closeOrderButton");
                OrderComplete = true;
                _logger.Info(" > Purchase Order completed!");
            }
            this.CartTable = HelperMethods.FindElement(driver, "xpath", "//tbody");
            this.ShipToDropdown = HelperMethods.FindElement(driver, "classname", "k-input");
            this.PONumberTextbox = HelperMethods.FindElement(driver, "id", "poNumber");
            this.POBuyerTextbox = HelperMethods.FindElement(driver, "id", "originalRefNumber");
            this.UnitsTextbox = HelperMethods.FindElement(driver, "id", "totalQuantityBox");
            this.AmountTextbox = HelperMethods.FindElement(driver, "id", "totalAmountBox");
            this.ContactNameTextbox = HelperMethods.FindElement(driver, "id", "contactName");
            this.DeliveryAddressDisplay = HelperMethods.FindElement(driver, "id", "addresseeName");
            this.FreightTermsTextbox = HelperMethods.FindElement(driver, "id", "freightTerms");
            this.PaymentTermsTextbox = HelperMethods.FindElement(driver, "id", "paymentTerms");
            this.NotesTextbox = HelperMethods.FindElement(driver, "id", "notes");
            #endregion

            if (!driver.Url.Contains("MyOrder") && !OrderComplete)
            {
                _logger.Fatal(" > MyCart navigation [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }               
        }

        /// <summary>
        /// Loads all the items currently in the cart and makes objects out of the 
        /// row information to interact with.
        /// </summary>
        internal void LoadItemsInCart()
        {
            _logger.Info(" > Attempting to load cart items...");

            // get row elements
            _itemDeleteButtons = CartTable.FindElements(By.XPath(Constants.ITEM_DEL_BTN_XP));
            _itemProdNums = CartTable.FindElements(By.XPath(Constants.ITEM_PN_XP));
            _itemDescriptions = CartTable.FindElements(By.XPath(Constants.ITEM_DES_XP));
            _itemPrices = CartTable.FindElements(By.XPath(Constants.ITEM_PRICE_XP));
            _itemQtys = CartTable.FindElements(By.XPath(Constants.ITEM_QTY_XP));
            _itemTotals = CartTable.FindElements(By.XPath(Constants.ITEM_TOTAL_XP));
            try
            {
                // make cart item objects to work with
                for (int i = 0; i < _itemProdNums.Count - 1; ++i)
                {
                    CartItem item = new CartItem();
                    item.DeleteButton = _itemDeleteButtons[i];
                    item.ProductNumber = _itemProdNums[i];
                    item.Description = _itemDescriptions[i];
                    item.Price = _itemPrices[i];
                    item.Quantity = _itemQtys[i];
                    item.ItemTotalAmt = _itemTotals[i];
                    _cartLineItems.Add(item);
                }
            }
            catch (Exception)
            {
                _logger.Fatal(" > Loading MyCart items [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
        }

        /// <summary>
        /// Find specific item in user's cart
        /// </summary>
        /// <param name="itemDes">description of item to be searched for</param>
        /// <returns>Cart object to interact with</returns>
        private CartItem LoadCartItem(string itemDes)
        {
            _logger.Info(" > Attempting to find cart item: " + itemDes);
            CartItem currentItem = new CartItem();
            try
            {
                for (int i = 0; i < _cartLineItems.Count; ++i)
                {
                    if (_itemDescriptions[i].Text.Equals(itemDes))
                    {
                        currentItem.DeleteButton = _itemDeleteButtons[i];
                        currentItem.ProductNumber = _itemProdNums[i];
                        currentItem.Description = _itemDescriptions[i];
                        currentItem.Price = _itemPrices[i];
                        currentItem.Quantity = _itemQtys[i];
                        currentItem.ItemTotalAmt = _itemTotals[i];
                        _logger.Info(" > Cart item found: " + itemDes);
                    }
                }
            } catch (Exception)
            {
                _logger.Fatal(" > Attempting to find MyCart item [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
            return currentItem;
        }

        internal bool VerifyItemsInCart(List<Product> prodsInCart)
        {
            int consistencyCount = 0;
            if (!prodsInCart.Count.Equals(_cartLineItems.Count))
                return false;
            else
            {
                foreach( CartItem cartLineItem in _cartLineItems )
                {
                    foreach ( Product prod in prodsInCart )
                    {
                        if (prod.ProductNumber.Equals(cartLineItem.ProductNumber))
                        {
                            if(prod.Price.Equals(cartLineItem.Price) &&
                                prod.Quantity.Equals(cartLineItem.Quantity))
                            {
                                double prodPrice = 0;
                                double cartLineItemTotal = 0;
                                Double.TryParse(prod.Price, out prodPrice);
                                Double.TryParse(cartLineItem.ItemTotalAmt.Text, out cartLineItemTotal);
                                double prodTotal = prodPrice * prod.Quantity;
                                if (prodTotal.Equals(cartLineItemTotal))
                                    consistencyCount++;
                            }
                        }
                        else
                            continue;
                    }
                }
                if (consistencyCount.Equals(prodsInCart.Count))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Simulate the deletion of an item from the cart
        /// </summary>
        /// <param name="pn">Find item by product number</param>
        /// <returns>Current page object</returns>
        public MyCartPage RemoveItemFromCart(string pn)
        {
            Thread.Sleep(1000);
            CartItem item;
            try
            {
                _logger.Info(" > Attempting to delete a cart item...");
                ItemDeleted = false;
                if (_cartLineItems.Any())
                {
                    foreach ( CartItem cItem in _cartLineItems )
                    {
                        if (cItem.ProductNumber.Text.Equals(pn))
                        {
                            item = LoadCartItem(cItem.Description.Text);
                            item.DeleteButton.Click();

                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath(Constants.XPATH_DEL_ITEM_OK)).Click();

                            // check alert for confirmation of delete
                            ItemDeleted = HelperMethods.CheckAlert(driver);

                            if (ItemDeleted)
                            {
                                _logger.Info(" > Cart item deleted! Item: " +pn);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    this.ItemDeleted = false;
                    _logger.Error(" > No items in cart!");
                }
            } catch (Exception)
            {
                _logger.Fatal(" > Removing item from MyCart [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
            }
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
            driver.FindElement(By.XPath(Constants.XPATH_INFO_OK)).Click();
            this.AlertSuccess = HelperMethods.CheckAlert(driver);
            return new ViewOrdersPage(driver);
        }

        public MyCartPage SaveDraftOrder()
        {
            this.AlertSuccess = false;

            // attempt to save
            SaveDraftButton.Click();
            Thread.Sleep(300);
            this.AlertSuccess = HelperMethods.CheckAlert(driver);
            if (AlertSuccess.Equals(Constants.MISSING_INFO_MSG)) // if po missing
            {
                // enter a po and attempt to save again
                EnterRandomPONumber(7);
                SaveDraftButton.Click();
                this.AlertSuccess = HelperMethods.CheckAlert(driver);
            }
            return this;
        }

        public MyCartPage SendOrder()
        {
            this.AlertSuccess = false;
            SendOrderButton.Click();
            if (HelperMethods.IsElementPresent(driver, By.ClassName("modal-content")))
            {
                // click OK on Information dialog
                Thread.Sleep(500);
                HelperMethods.FindElement(driver, "xpath", Constants.XPATH_ORDER_OK).Click();
                //this.AlertSuccess = HelperMethods.CheckAlert(driver);
                // click Finish on next dialog
                Thread.Sleep(4000);
                HelperMethods.FindElement(driver, "xpath", Constants.XPATH_INFO_FINISH).Click();
                OrderComplete = true;
                return new MyCartPage(driver, "order_complete");
            }
            else
            {
                _logger.Fatal(" > Completing order [FAILED]");
                _logger.Fatal("-- TEST FAILURE @ URL: '" + driver.Url + "' --");
                BaseDriverTest.TakeScreenshot("screenshot");
                return this;
            }
        }
   
        public ViewOrdersPage CompleteOrder()
        {
            Thread.Sleep(1000);
            CloseOrderButton.Click();
            return new ViewOrdersPage(driver);
        }

        public void LoadReportsOptions()
        {
            this.ExportAsExcelOption = HelperMethods.FindElement(driver, "id", "excelExportOrderButton");
            this.OrderReportOption = HelperMethods.FindElement(driver, "id", "orderReportButton");
            this.OrderSummaryOption = HelperMethods.FindElement(driver, "id", "orderSummaryButton");
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
