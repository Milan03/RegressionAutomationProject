namespace DocumentCentreTests.Util
{
    /// <summary>contains constants used by the tests such as the user name and password for the sauce labs</summary>
    internal static class Constants
    {
        #region Group Portal Login Information
        internal const string SA_MEMBER_USER = "test01dealer";
        internal const string SA_MEMBER_PASS = "imatestthis";
        internal const string SA_SUPPLIER_USER = "test01vendor";
        internal const string SA_SUPPLIER_PASS = "imatestthis";
        internal const string TAF_MEMBER_USER = "TES9GPTFC5";
        internal const string TAF_MEMBER_PASS = "2RTTL4SZ";
        internal const string DRAKE_MEMBER_USER = "Gry2HNG47A";
        internal const string DRAKE_MEMBER_PASS = "KX5YEKWR";
        internal const string DOWNLOAD_PATH = "C:\\logging\\downloads";
        #endregion

        #region UI Messages
        internal const string LOGIN_ERROR_MSG = "The combination of username and password is incorrect";
        internal const string ORDER_ERROR_MSG = "Nothing to show";
        internal const string ORDER_DELETE_MSG = "Order has been deleted.";
        internal const string MISSING_INFO_MSG = "Some information is missing or invalid.";
        internal const string ORDER_COMPLETE_MSG = "Order has been sent for fulfillment.";
        internal const string CAT_PAGE_TITLE = "Select a Catalog to Order Products";
        internal const string TEST_CAT = "MILAN AUTOMATION CATALOGUE";
        #endregion

        #region Order Statuses/View Orders Page
        internal const string ORDER_SEARCH_DRAFT = "Draft";
        internal const string ORDER_SEARCH_PENDING = "Pending Approval";
        internal const string ORDER_SEARCH_SENT = "Sent";
        internal const string ORDER_SEARCH_PROC = "Processing";
        internal const string ORDER_SEARCH_DEL = "Delivered";
        internal const string ORDER_PO_PROC = "0000019";
        internal const string INVALID_PO = "asdf1234adfs";
        #endregion

        #region Products Page
        internal const string PROD_ROW_WRAPPERS_XP = "//div[contains(@class, 'product-row-wrapper')]";
        internal const string ALL_PROD_VARIANTS_XP = "div[id^='variant_']";
        internal const string PROD_VAR_QTYS_XP = "//div[contains(@id, 'variant')]/div[3]/span/span/input[2]";
        internal const string ROW_TITLE_XP = "//tbody/tr/td[2]/div/a[1]";
        internal const string ALL_QTY_BOXES_XP = "//input[contains(@class,'k-formatted-value')]";
        internal const string ROW_QTY_UP_XP = "//span[contains(@class,'k-i-arrow-n')]";
        internal const string ROW_QTY_DOWN_XP = "//span[contains(@class,'k-i-arrow-s')]";
        internal const string ROW_PRICE_XP = "//[contains(@class, 'price') and contains(@class ,'Hover')]";
        internal const string ROW_UPDATE_XP = "//tbody/tr/td[2]/div[2]/div[2]/button";
        #endregion

        #region Cart Row Identifiers
        internal const string ITEM_DEL_BTN_XP = "//tbody/tr/td/div/button[3]";
        internal const string ITEM_PN_XP = "//tbody/tr/td[3]";
        internal const string ITEM_PN_CELLS_XP = "//tbody/tr/td[2]";
        internal const string ITEM_PN_TB_XP = "//tbdoy/tr/td[2]/input";
        internal const string ITEM_PRICE_XP = "//tbody/tr/td[7]";
        internal const string ITEM_QTY_XP = "//tbody/tr/td[8]";
        internal const string ITEM_TOTAL_XP = "//tbody/tr/td[9]";
        internal const string FR_TB_XP = "//td/input[contains(@class,'k-textbox')]";
        internal const string CART_ORDER_GRID = "mainOrderScreenTabs-2";
        internal const string EDITABLE_ROW_XP = "//td[@class='editable']";
        internal const string ACTIVE_ROW_QTY_XP = "//td[contains(@class,'editable')]//span[contains(@class,'k-i-arrow-n')]";
        #endregion

        #region PO Locators
        internal const string CART_NEW = "new_order";
        internal const string CART_COMPLETE = "order_complete";
        internal const string PO_LOCATOR_XP = "id('ordersGrid')/div[2]/table/tbody/tr[1]/td[3]";
        internal const string DEL_ORDER_XP = "//button[contains(@class, 'btn-delete')]";
        internal const string EDIT_ORDER_XP = "//button[contains(@class, 'btn-copy')]";
        internal const string INFO_OK_XP = "//button[.='OK']";
        internal const string ORDER_OK_XP = "//div[@class='bootstrap-dialog-footer-buttons']/button[2]";
        internal const string INFO_PRINT_XP = "//button[.='Print']";
        internal const string INFO_EXPORT_XP = "//button[.='Export']";
        internal const string INFO_EMAIL_XP = "//button[.='Email']";
        internal const string INFO_FINISH_XP = "//div[@class='bootstrap-dialog-footer-buttons']/button[4]";
        internal const string ALERT_SUCCESS_XP = "//div[contains(@class, 'toast-success')]";
        internal const string ALERT_FAILURE_XP = "//div[contains(@class, 'toast-failure')]";
        internal const string CAT_LOCATOR_XP = "//h1[contains(@class, 'catalog-tile-text') and contains (text(), 'Milan Automation Catalogue')]";
        internal const string REPORTS_LOCATOR_XP = "//button[contains(text(), 'Reports')]";
        internal const string SHIPON_CAL_XP = "id('orderHeaderContainer')/div[2]/div[1]/div/span/span/span/span";
        internal const string CANCELAFTER_CAL_XP = "id('orderHeaderContainer')/div[2]/div[2]/div/span/span/span/span";
        internal const string MYCART_LINK_XP = "//span[contains(text(), 'My Cart')]";
        internal const string DEL_ITEM_OK_XP = "//div[contains(@class, 'modal-footer')]/div/div/button[2]";
        #endregion

        #region Mailbox IDs and Locators
        internal const string STATUS_DD_XP = "//span[contains(@title,'PO Received by status')]//span[contains(@class,'k-i-arrow-s')]";
        internal const string PERIOD_DD_XP = "//span[contains(@title,'PO Received by year')]//span[contains(@class,'k-i-arrow-s')]";
        internal const string QS_TEXTBOX_ID = "searchTerm";
        internal const string QS_BTN_ID = "invoiceSearchButton";
        internal const string AS_LINK_ID = "advancedSearchLink";
        internal const string PRINT_BTN_ID = "toolbarPrintButton";
        internal const string MP_BTN_ID = "toolbarProcessButton";
        internal const string ACTIONS_BTN_ID = "toolbarActions";
        internal const string ACTIONS_MP_ID = "actionsMarkProcessed";
        internal const string ACTIONS_MUP_ID = "actionsMarkUnprocessed";
        internal const string ACTIONS_PRINT_ID = "reportsPrintGrid";
        internal const string ACTIONS_OPTIONS_ID = "actionsOptions";
        internal const string RSLT_GRID_ID = "searchResults";
        internal const string AS_SEARCH_BTN_ID = "advancedSearchButton";
        internal const string AS_CLEAR_BTN_ID = "clearAdvancedSearchLink";
        internal const string AS_BASIC_BTN_ID = "basicSearchLink";
        internal const string STATUS_ALL_XP = "//div[@id='processStatusList-list']//li[.='All']";
        internal const string STATUS_PROCESSED_XP = "//div[@id='processStatusList-list']//li[.='Processed']";
        internal const string STATUS_UNPROCESSED_XP = "//div[@id='processStatusList-list']//li[.='Unprocessed']";
        internal const string PERIOD_COUNT_XP = "//ul[@id='yearsList_listbox']//li";
        internal const string PERIOD_LAST90_XP = "//ul[@id='yearsList_listbox']//li[.='Last 90 Days']";
        internal const string PERIOD_2016_XP = "//ul[@id='yearsList_listbox']//li[.='2016']";
        internal const string PERIOD_2015_XP = "//ul[@id='yearsList_listbox']//li[.='2015']";
        internal const string PERIOD_2014_XP = "//ul[@id='yearsList_listbox']//li[.='2014']";
        internal const string PERIOD_2013_XP = "//ul[@id='yearsList_listbox']//li[.='2013']";
        internal const string PERIOD_2012_XP = "//ul[@id='yearsList_listbox']//li[.='2012']";
        internal const string PERIOD_2011_XP = "//ul[@id='yearsList_listbox']//li[.='2011']";
        internal const string PERIOD_2010_XP = "//ul[@id='yearsList_listbox']//li[.='2010']";
        internal const string PERIOD_2009_XP = "//ul[@id='yearsList_listbox']//li[.='2009']";
        internal const string PERIOD_2008_XP = "//ul[@id='yearsList_listbox']//li[.='2008']";
        #endregion

        #region Mailbox Enums
        internal enum GridElementsToDisplay
        {
            Ten = 10,
            Twenty = 20,
            Fifty = 50,
            Hundred = 100
        };
        internal enum SearchStatus
        {
            All,
            Processed,
            Unprocessed
        }
        internal enum PeriodYear
        {
            Last90,
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
        #endregion

        #region PO Mailbox Locators
        internal const string AS_FROM_XP = "//span[contains(@title,'From')]//span[contains(@class,'k-i-arrow-s')]";
        internal const string AS_SHIP_TO_XP = "//span[contains(@title,'Ship To')]//span[contains(@class,'k-i-arrow-s')]";
        internal const string PO_CHECKBOXES_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[1]";
        internal const string PO_STATUSES_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[2]";
        internal const string PO_SENDER_NAMES_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[3]";
        internal const string PO_SHIP_TOS_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[4]";
        internal const string PO_PONUMBERS_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[5]";
        internal const string PO_PODATES_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[6]";
        internal const string PO_BILL_TO_NAMES_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[7]";
        internal const string PO_TOTAL_AMOUNTS_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[8]";
        internal const string PO_DATES_ADDED_XP = "//div[contains(@class,'k-grid-content')]//tbody/tr/td[9]";
        internal const string FIRST_PAGE_NAV_XP = "//a[contains(@title,'first page')]";
        internal const string PREV_PAGE_NAV_XP = "//a[contains(@title,'previous page')]";
        internal const string NEXT_PAGE_NAV_XP = "//a[contains(@title,'next page')]";
        internal const string LAST_PAGE_NAV_XP = "//a[contains(@title,'last page')]";
        internal const string PAGE_NUM_XP = "//span[contains(@class,'k-pager-nav')]";
        internal const string PAGE_DROPDOWN_XP = "//span[contains(@class,'k-pager-sizes')]//span[contains(@class,'k-i-arrow-s')]";
        internal const string PAGE_INFO_LBL_XP = "//span[contains(@class,'k-pager-info')]";
        internal const string PAGE_REFRESH_XP = "//span[contains(@class,'k-i-refresh')]";
        internal const string PAGE_AMT_10 = "//li[text()='10']";
        internal const string PAGE_AMT_20 = "//li[text()='20']";
        internal const string PAGE_AMT_50 = "//li[text()='50']";
        internal const string PAGE_AMT_100 = "//li[text()='100']";
        #endregion

        #region Supplier Home Page
        internal const string ORDER_FULFILLMENT = "Order Fulfillment";
        internal const string VIEW_POS = "View POs";
        #endregion
    }
}
