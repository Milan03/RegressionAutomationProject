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
    internal class CartItem
    {
        internal IWebElement DeleteButton { get; set; }
        internal IWebElement ProductNumber { get; set; }
        internal IWebElement Description { get; set; }
        internal IWebElement Price { get; set; }
        internal IWebElement Quantity { get; set; }
        internal IWebElement ItemTotalAmt { get; set; }
        internal bool Checked { get; set; }

        internal CartItem() { }
    }
}
