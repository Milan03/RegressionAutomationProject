using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Catalogue
{
    internal class Product
    {
        internal string SupplierProductNumber { get; set; }
        internal string Description { get; set; }
        internal Decimal Price { get; set; }
        internal int Quantity { get; set; }
        internal Decimal AmountTotal { get; set; }

        internal Product()
        {
            this.SupplierProductNumber = "";
            this.Description = "";
            this.Price = 0;
            this.Quantity = 0;
            this.AmountTotal = 0;
        }

        internal Product(string prodNum, string des, Decimal price, int qty)
        {
            this.SupplierProductNumber = prodNum;
            this.Description = des;
            this.Price = price;
            this.Quantity = qty;
            this.AmountTotal = price * qty;
        }
    }
}