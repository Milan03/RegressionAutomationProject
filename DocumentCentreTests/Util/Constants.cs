namespace DocumentCentreTests.Util
{
    /// <summary>contains constants used by the tests such as the user name and password for the sauce labs</summary>
    internal static class Constants
    {
        internal static class Affiliation
        {
            internal static class SA
            {
                internal const string USER = "Supply America User";
                internal const string MEMBER_USER = "test01dealer";
                internal const string MEMBER_PASS = "imatestthis";
                internal const string SUPPLIER_USER = "test01vendor";
                internal const string SUPPLIER_PASS = "imatestthis";
            }
            internal static class Drake
            {
                internal const string USER = "Drake User";
                internal const string MEMBER_USER = "DraAFC7J9E";
                internal const string MEMBER_PASS = "WTRPM2BN";
            }
            internal static class TAF
            {
                internal const string USER = "TAF User";
                internal const string MEMBER_USER = "TES9GPTFC5";
                internal const string MEMBER_PASS = "2RTTL4SZ";
            }
        }
        internal static class UIMessages
        {
            internal const string LOGIN_ERROR = "The combination of username and password is incorrect";
            internal const string ORDER_ERROR = "Nothing to show";
            internal const string ORDER_DELETE = "Order has been deleted.";
            internal const string MISSING_INFO = "Some information is missing or invalid.";
            internal const string ORDER_COMPLETE = "Order has been sent for fulfillment.";
        }
        internal static class OrderStatus
        {
            internal const string ALL = "All";
            internal const string DRAFT = "Draft";
            internal const string PENDING = "Pending Approval";
            internal const string SENT = "Sent";
            internal const string PROCESSING = "Processing";
            internal const string DELIVERED = "Delivered";
        }
        internal static class ProductPage
        {
            internal static class XP
            {
                internal const string PROD_ROW_WRAPPERS = "//div[contains(@class, 'product-row-wrapper')]";
                internal const string ALL_PROD_VARIANTS = "div[id^='variant_']";
                internal const string PROD_VAR_QTYS = "//div[contains(@id, 'variant')]/div[3]/span/span/input[2]";
                internal const string ROW_TITLE = "//tbody/tr/td[2]/div/a[1]";
                internal const string ALL_QTY_BOXES = "//input[contains(@class,'k-formatted-value')]";
                internal const string ROW_QTY_UP = "//span[contains(@class,'k-i-arrow-n')]";
                internal const string ROW_QTY_DOWN = "//span[contains(@class,'k-i-arrow-s')]";
                internal const string ROW_PRICE = "//[contains(@class, 'price') and contains(@class ,'Hover')]";
                internal const string ROW_UPDATE = "//tbody/tr/td[2]/div[2]/div[2]/button";
            }
        }
        internal static class MyCart
        {
            internal static class XP
            {
                internal const string ITEM_DEL_BTN = "//tbody/tr/td/div/button[3]";
                internal const string ITEM_PN = "//tbody/tr/td[3]";
                internal const string ITEM_PN_CELLS = "//tbody/tr/td[2]";
                internal const string ITEM_PN_TB = "//tbdoy/tr/td[2]/input";
                internal const string ITEM_PRICE = "//tbody/tr/td[7]";
                internal const string ITEM_QTY = "//tbody/tr/td[8]";
                internal const string ITEM_TOTAL = "//tbody/tr/td[9]";
                internal const string FR_TB = "//td/input[contains(@class,'k-textbox')]";
                internal const string CART_ORDER_GRID = "bgHover";
                internal const string EDITABLE_ROW = "//td[@class='editable']";
                internal const string ACTIVE_ROW_QTY = "//td[contains(@class,'editable')]//span[contains(@class,'k-i-arrow-n')]";
            }
        }
        internal static class PO
        {
            internal static class XP
            {
                internal const string PO_LOCATOR = "id('ordersGrid')/div[2]/table/tbody/tr[1]/td[3]";
                internal const string DEL_ORDER = "//button[contains(@class, 'btn-delete')]";
                internal const string EDIT_ORDER = "//button[contains(@class, 'btn-copy')]";
                internal const string INFO_OK = "//button[.='OK']";
                internal const string ORDER_OK = "//div[@class='bootstrap-dialog-footer-buttons']/button[2]";
                internal const string INFO_PRINT = "//button[.='Print']";
                internal const string INFO_EXPORT = "//button[.='Export']";
                internal const string INFO_EMAIL = "//button[.='Email']";
                internal const string INFO_FINISH = "//div[@class='bootstrap-dialog-footer-buttons']/button[4]";
                internal const string ALERT_SUCCESS = "//div[contains(@class, 'toast-success')]";
                internal const string ALERT_FAILURE = "//div[contains(@class, 'toast-failure')]";
                internal const string CAT_LOCATOR = "//h1[contains(@class, 'catalog-tile-text') and contains (text(), 'Milan Automation Catalogue')]";
                internal const string REPORTS_LOCATOR = "//button[contains(text(), 'Reports')]";
                internal const string SHIPON_CAL = "id('orderHeaderContainer')/div[2]/div[1]/div/span/span/span/span";
                internal const string CANCELAFTER_CAL = "id('orderHeaderContainer')/div[2]/div[2]/div/span/span/span/span";
                internal const string MYCART_LINK = "//span[contains(text(), 'My Cart')]";
                internal const string DEL_ITEM_OK = "//div[contains(@class, 'modal-footer')]/div/div/button[2]";
            }
        }
        internal static class BaseMailbox
        {
            internal static class XP
            {
                internal const string STATUS_DD = "//span[contains(@title,'PO Received by status')]//span[contains(@class,'k-i-arrow-s')]";
                internal const string PERIOD_DD = "//span[contains(@title,'PO Received by year')]//span[contains(@class,'k-i-arrow-s')]";
                internal const string STATUS_ALL = "//div[@id='processStatusList-list']//li[.='All']";
                internal const string STATUS_PROCESSED = "//div[@id='processStatusList-list']//li[.='Processed']";
                internal const string STATUS_UNPROCESSED = "//div[@id='processStatusList-list']//li[.='Unprocessed']";
                internal const string PERIOD_COUNT = "//ul[@id='yearsList_listbox']//li";
                internal const string PERIOD_LAST90 = "//ul[@id='yearsList_listbox']//li[.='Last 90 Days']";
                internal const string PERIOD_2016 = "//ul[@id='yearsList_listbox']//li[.='2016']";
                internal const string PERIOD_2015 = "//ul[@id='yearsList_listbox']//li[.='2015']";
                internal const string PERIOD_2014 = "//ul[@id='yearsList_listbox']//li[.='2014']";
                internal const string PERIOD_2013 = "//ul[@id='yearsList_listbox']//li[.='2013']";
                internal const string PERIOD_2012 = "//ul[@id='yearsList_listbox']//li[.='2012']";
                internal const string PERIOD_2011 = "//ul[@id='yearsList_listbox']//li[.='2011']";
                internal const string PERIOD_2010 = "//ul[@id='yearsList_listbox']//li[.='2010']";
                internal const string PERIOD_2009 = "//ul[@id='yearsList_listbox']//li[.='2009']";
                internal const string PERIOD_2008 = "//ul[@id='yearsList_listbox']//li[.='2008']";
            }
            internal static class ID
            {
                internal const string QS_TEXTBOX = "searchTerm";
                internal const string QS_BTN = "invoiceSearchButton";
                internal const string AS_LINK = "advancedSearchLink";
                internal const string PRINT_BTN = "toolbarPrintButton";
                internal const string MP_BTN = "toolbarProcessButton";
                internal const string ACTIONS_BTN = "toolbarActions";
                internal const string ACTIONS_MP = "actionsMarkProcessed";
                internal const string ACTIONS_MUP = "actionsMarkUnprocessed";
                internal const string ACTIONS_PRINT = "reportsPrintGrid";
                internal const string ACTIONS_OPTIONS = "actionsOptions";
                internal const string RSLT_GRID = "searchResults";
                internal const string AS_SEARCH_BTN = "advancedSearchButton";
                internal const string AS_CLEAR_BTN = "clearAdvancedSearchLink";
                internal const string AS_BASIC_BTN = "basicSearchLink";
            }
            internal static class Enums
            {
                internal enum GridElementsToDisplay
                {
                    TEN = 10,
                    TWENTY = 20,
                    FIFTY = 50,
                    HUNDRED = 100
                };
                internal enum SearchStatus
                {
                    ALL,
                    PROCESSED,
                    UNPROCESSED
                }
                internal enum PeriodYear
                {
                    LAST90,
                    _2017,
                    _2016,
                    _2015,
                    _2014,
                    _2013,
                    _2012,
                    _2011,
                    _2010,
                    _2009,
                    _2008
                }
            }
        }
        internal static class POMailbox
        {
            internal static class XP
            {
                internal const string AS_FROM = "//span[contains(@title,'From')]//span[contains(@class,'k-i-arrow-s')]";
                internal const string AS_SHIP_TO = "//span[contains(@title,'Ship To')]//span[contains(@class,'k-i-arrow-s')]";
                internal const string PO_CHECKBOXES = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[1]";
                internal const string PO_STATUSES = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[2]";
                internal const string PO_SENDER_NAMES = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[3]";
                internal const string PO_SHIP_TOS = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[4]";
                internal const string PO_PONUMBERS = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[5]";
                internal const string PO_PODATES = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[6]";
                internal const string PO_BILL_TO_NAMES = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[7]";
                internal const string PO_TOTAL_AMOUNTS = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[8]";
                internal const string PO_DATES_ADDED = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[9]";
                internal const string FIRST_PAGE_NAV = "//a[contains(@title,'first page')]";
                internal const string PREV_PAGE_NAV = "//a[contains(@title,'previous page')]";
                internal const string NEXT_PAGE_NAV = "//a[contains(@title,'next page')]";
                internal const string LAST_PAGE_NAV = "//a[contains(@title,'last page')]";
                internal const string PAGE_NUM = "//span[contains(@class,'k-pager-nav')]";
                internal const string PAGE_DROPDOWN = "//span[contains(@class,'k-pager-sizes')]//span[contains(@class,'k-i-arrow-s')]";
                internal const string PAGE_INFO_LBL = "//span[contains(@class,'k-pager-info')]";
                internal const string PAGE_REFRESH = "//span[contains(@class,'k-i-refresh')]";
                internal const string PAGE_AMT_10 = "//li[text()='10']";
                internal const string PAGE_AMT_20 = "//li[text()='20']";
                internal const string PAGE_AMT_50 = "//li[text()='50']";
                internal const string PAGE_AMT_100 = "//li[text()='100']";
            }
        }
        internal static class Text
        {
            internal const string ORDER_FULFILLMENT = "Order Fulfillment";
            internal const string VIEW_POS = "View POs";
            internal const string VIEW_DRAFT_ORDERS = "View Draft Orders";
            internal const string INVALID_PO = "asdf1234adfs";
            internal const string ORDER_PO_PROC = "0000019";
            internal const string CAT_PAGE_TITLE = "Select a Catalog to Order Products";
            internal const string TEST_CAT = "MILAN AUTOMATION CATALOGUE";
        }
        internal static class OrderType
        {
            internal const string NEW = "new_order";
            internal const string COMPLETE = "order_complete";
        }
        internal static class Internal
        {
            internal const string DOWNLOAD_PATH = "C:\\logging\\downloads";
        }
    }
}
