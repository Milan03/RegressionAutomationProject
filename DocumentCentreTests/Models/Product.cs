using OpenQA.Selenium;
using System;

namespace DocumentCentreTests.Models
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

        internal void SetQuantity(int qty)
        {
            Quantity = qty;
            for (int i = 0; i < qty; ++i)
            {
                QtyUp.Click();
            }
        }

        internal decimal getAmountTotal()
        {
            var priceStr = Price;
            return Quantity * decimal.Parse(priceStr.Substring(2, priceStr.Length - 2));
        }
    }
}