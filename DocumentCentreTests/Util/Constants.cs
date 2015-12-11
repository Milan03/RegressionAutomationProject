using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Util
{
     /// <summary>contains constants used by the tests such as the user name and password for the sauce labs</summary>
    internal static class Constants
    {
        /// <summary>member portal login info
        internal const string MEM_PORTAL_USER = "test01dealer";
        internal const string MEM_PORTAL_PASS = "imatestthis";

        /// <summary>UI messages
        internal const string LOGIN_ERROR_MSG = "The combination of username and password is incorrect";
        internal const string ORDER_ERROR_MSG = "Nothing to show";
        internal const string ORDER_DELETE_MSG = "Order has been deleted.";
        internal const string MISSING_INFO_MSG = "Some information is missing or invalid.";
        internal const string ORDER_COMPLETE_MSG = "Order has been sent for fulfillment.";

        internal const string CAT_PAGE_TITLE = "Select a Catalog to Order Products";
        internal const string TEST_CAT = "MILAN AUTOMATION CATALOGUE";

        /// <summary>search options to be used with View Orders page
        internal const string ORDER_SEARCH_DRAFT = "Draft";
        internal const string ORDER_SEARCH_PENDING = "Pending Approval";
        internal const string ORDER_SEARCH_SENT = "Sent";
        internal const string ORDER_SEARCH_PROC = "Processing";
        internal const string ORDER_SEARCH_DEL = "Delivered";

        /// <summary>individual PO #s to be used while testing
        internal const string ORDER_PO_PROC = "0000019";
        internal const string INVALID_PO = "asdf1234adfs";

        /// <summary>row identifiers
        internal const string ROW_TITLE_XPATH = "//tbody/tr/td[2]/div/a[1]";
        internal const string ROW_QTY_UP_XPATH = "//tbody/tr/td[2]/div[2]/div/div/span/span/span/span[1]";
        internal const string ROW_QTY_DOWN_XPATH = "//tbody/tr/td[2]/div[2]/div/div/span/span/span/span[2]";
        internal const string ROW_PRICE_XPATH = "//tbody/tr/td[2]/div[2]/div/div[2]";
        internal const string ROW_UPDATE_XPATH = "//tbody/tr/td[2]/div[2]/div[2]/button";

        /// <summary>XPath locators
        internal const string XPATH_PO_LOCATOR = "id('ordersGrid')/div[2]/table/tbody/tr[1]/td[3]";
        internal const string XPATH_DEL_ORDER = "id('ordersGrid')/div[2]/table/tbody/tr[2]/td[1]/div/button[2]";
        internal const string XPATH_INFO_OK = "//button[contains(.,'OK')]";
        internal const string XPATH_INFO_PRINT = "//button[contains(text(), 'Print')]";
        internal const string XPATH_INFO_EXPORT = "//button[contains(text(), 'Export')]";
        internal const string XPATH_INFO_EMAIL = "//button[contains(text(), 'Email')]";
        internal const string XPATH_INFO_FINISH = "//button[contains(text(), 'Finish')]";
        internal const string XPATH_ALERT_MSG = "id('toast-container')/div/div";
        internal const string XPATH_CAT_LOCATOR = "//h1[contains(@class, 'catalog-tile-text') and contains (text(), 'Milan Automation Catalogue')]";
        internal const string XPATH_REPORTS_LOCATOR = "//button[contains(text(), 'Reports')]";
        internal const string XPATH_SHIPON_CAL = "id('orderHeaderContainer')/div[2]/div[1]/div/span/span/span/span";
        internal const string XPATH_CANCELAFTER_CAL = "id('orderHeaderContainer')/div[2]/div[2]/div/span/span/span/span";
        internal const string XPATH_MYCART_LINK = "//span[contains(text(), 'My Cart')]";
    }
}
