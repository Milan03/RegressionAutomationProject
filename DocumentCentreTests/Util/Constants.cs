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

        internal const string CAT_PAGE_TITLE = "Select a Catalog to Order Products";
        internal const string SA_TEST_CAT = "SUPPLY AMERICA TEST CATALOG";

        /// <summary>search options to be used with View Orders page
        internal const string ORDER_SEARCH_DRAFT = "Draft";
        internal const string ORDER_SEARCH_PENDING = "Pending Approval";
        internal const string ORDER_SEARCH_SENT = "Sent";
        internal const string ORDER_SEARCH_PROC = "Processing";
        internal const string ORDER_SEARCH_DEL = "Delivered";

        /// <summary>individual PO #s to be used while testing
        internal const string ORDER_PO_PROC = "0000019";
        internal const string INVALID_PO = "asdf1234adfs";

        /// <summary>XPath locators
        internal const string XPATH_PO_LOCATOR = "id('ordersGrid')/div[2]/table/tbody/tr[1]/td[3]";
        internal const string XPATH_DEL_ORDER = "id('ordersGrid')/div[2]/table/tbody/tr[2]/td[1]/div/button[2]";
        internal const string XPATH_INFO_OK = "//button[contains(.,'OK')]";
        internal const string XPATH_ALERT_MSG = "id('toast-container')/div/div";
        internal const string XPATH_CAT_LOCATOR = "//h1[contains(@class, 'catalog-tile-text') and contains (text(), 'Supply America Test Catalog')]";
        internal const string XPATH_PRODUCTS_TAB = "//a[contains(@class, 'k-link') and contains (text(), 'Products')]";
    }
}
