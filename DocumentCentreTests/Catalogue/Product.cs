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
        internal IWebElement ProductTitle { get; set; }
        internal IWebElement Price { get; set; }
        internal IWebElement Quantity { get; set; }
        internal IWebElement UpdateButton { get; set; }
        internal Decimal AmountTotal
        {
            get { return this.AmountTotal;  }
            set
            {
                this.AmountTotal = Decimal.Parse(Price.Text) * int.Parse(Quantity.Text);
            }
        }

        internal Product() {}

        public void SetQuantity(string qty)
        {
            Quantity.SendKeys(qty);
        }
    }
}