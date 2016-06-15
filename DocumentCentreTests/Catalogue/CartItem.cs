using OpenQA.Selenium;

namespace DocumentCentreTests.Catalogue
{
    internal class CartItem
    {
        internal IWebElement DeleteButton { get; set; }
        internal IWebElement ProductNumber { get; set; }
        internal IWebElement PNCell { get; set; }
        internal IWebElement PNTextbox { get; set; }
        internal IWebElement Description { get; set; }
        internal IWebElement Price { get; set; }
        internal IWebElement Quantity { get; set; }
        internal IWebElement ItemTotalAmt { get; set; }
        internal bool Checked { get; set; }

        internal CartItem() { }
    }
}
