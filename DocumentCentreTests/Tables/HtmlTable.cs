using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DocumentCentreTests.Tables
{
    /// <summary>
    /// From: http://www.selenium-automation.co.uk/blog/html-table-navigation-with-webdriver-linq-and-generic-types/
    /// This is an abstract representation of a basic HTML table. We know a table is a collection of rows and if they all have
    /// the same amount of columns we can generify it in code
    /// </summary>
    /// <typeparam name="T">The type of table row</typeparam>
    public abstract class HtmlTable<T>
    {
        protected IWebElement baseElement;

        protected Func<IWebElement, T> constructRowDelegate;

        /// <summary>
        /// Ctor for an HTML table of a particular row type
        /// </summary>
        /// <param name="baseElement">The base Web element of the table i.e The table tag element</param>
        /// <param name="constructRowDelegate">A delegate function that constructs a row of the table row type</param>
        public HtmlTable(IWebElement baseElement, Func<IWebElement, T> constructRowDelegate)
        {
            this.baseElement = baseElement;
            this.constructRowDelegate = constructRowDelegate;
        }

        /// <summary>
        /// Returns a collection of all rows in the HTML table as the specified in the generic type paramter
        /// </summary>
        /// <returns>A collection of rows as row objects</returns>
        public IEnumerable<T> GetAllRows()
        {
            return this.baseElement.FindElements(By.TagName("tr")).Select(baseElement => constructRowDelegate(baseElement));
        }
    }
}
