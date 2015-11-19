using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace DocumentCentreTests.Pages
{
    /// <summary>
    /// Abstract class representing generic home page (can be either member, vendor, or group).
    /// Abstract functionality to share common return types for method returns/signatures.
    /// </summary>
    public abstract class HomePage
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();

        public abstract ViewOrdersPage NavigateToViewOrders();
    }
}
