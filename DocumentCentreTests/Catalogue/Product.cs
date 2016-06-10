using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Catalogue
{
    internal class Product
    {
        internal string Price { get; set; }
        internal string Colour { get; set; }
        internal string Size { get; set; }
        internal string ProductNumber { get; set; }
        internal string UPC { get; set; }
        internal IWebElement QtyBox { get; set; }
        internal IWebElement QtyUp { get; set; }
        internal IWebElement QtyDown { get; set; }
        internal IWebElement UpdateButton { get; set; }
        internal int Quantity { get; set; }
        internal IWebElement QtyLocator { get; set; }
        internal bool Checked { get; set; }

        internal Product() {}

        public void SetQuantity(int qty)
        {
            this.Quantity = qty;
            for (int i = 0; i < qty; ++i)
            {
                QtyUp.Click();
            }
        }

        public Decimal getAmountTotal()
        {
            var priceStr = Price;
            return Quantity * Decimal.Parse(priceStr.Substring(2, priceStr.Length - 2));
        }
    }
}