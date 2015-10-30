using Gallio.Framework;
using Gallio.Model;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DocumentCentreTests.Tables
{
    public class OrderTable : HtmlTable<OrderTable.OrderRow>
    {
        public OrderTable(IWebElement baseElement) :
            base(baseElement, el => new OrderRow(el)) { }

        public IEnumerable<OrderRow> FindRowsByPoNumber(string poNumber)
        {
            return this.GetAllRows().Where(row => row.PONumber == poNumber);
        }

        public class OrderRow
        {
            public IWebElement baseElement;

            public OrderRow(IWebElement baseElement)
            {
                this.baseElement = baseElement;
            }

            public string PONumber
            {
                get
                {
                    // cell index 1 holds the po number in table
                    return this.baseElement.FindElements(By.TagName("td"))[1].Text;
                }
            }

            public string Status
            {
                get
                {
                    // cell index 0 holds the status in table
                    return this.baseElement.FindElements(By.TagName("td"))[0].Text;
                }
            }
        }
    }
}
