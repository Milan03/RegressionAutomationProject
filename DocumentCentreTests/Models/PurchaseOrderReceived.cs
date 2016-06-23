using OpenQA.Selenium;
using System;

namespace DocumentCentreTests.Models
{
    public class PurchaseOrderReceived
    {
        internal IWebElement Checkbox { get; set; }
        internal string Status { get; set; }
        internal string SenderName { get; set; }
        internal string ShipToName { get; set; }
        internal string PONumber { get; set; }
        internal DateTime PODate { get; set; }
        internal Decimal TotalAmount { get; set; }
        internal string BillToName { get; set; }
        internal DateTime DateAdded { get; set; }
        
        internal PurchaseOrderReceived() { } 
    }
}
