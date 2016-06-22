namespace DocumentCentreTests.Util
{
    /// <summary>contains constants used by the tests such as the user name and password for the sauce labs</summary>
    internal static class Constants
    {
        /// <summary>member portal login info</summary>
        internal const string SA_MEMBER_USER = "test01dealer";
        internal const string SA_MEMBER_PASS = "imatestthis";
        internal const string SA_SUPPLIER_USER = "test01vendor";
        internal const string SA_SUPPLIER_PASS = "imatestthis";

        /// <summary>UI messages</summary>
        internal const string LOGIN_ERROR_MSG = "The combination of username and password is incorrect";
        internal const string ORDER_ERROR_MSG = "Nothing to show";
        internal const string ORDER_DELETE_MSG = "Order has been deleted.";
        internal const string MISSING_INFO_MSG = "Some information is missing or invalid.";
        internal const string ORDER_COMPLETE_MSG = "Order has been sent for fulfillment.";

        internal const string CAT_PAGE_TITLE = "Select a Catalog to Order Products";
        internal const string TEST_CAT = "MILAN AUTOMATION CATALOGUE";

        /// <summary>search options to be used with View Orders page</summary>
        internal const string ORDER_SEARCH_DRAFT = "Draft";
        internal const string ORDER_SEARCH_PENDING = "Pending Approval";
        internal const string ORDER_SEARCH_SENT = "Sent";
        internal const string ORDER_SEARCH_PROC = "Processing";
        internal const string ORDER_SEARCH_DEL = "Delivered";

        /// <summary>individual PO #s to be used while testing</summary>
        internal const string ORDER_PO_PROC = "0000019";
        internal const string INVALID_PO = "asdf1234adfs";

        /// <summary>products page row identifiers</summary>
        internal const string PROD_ROW_WRAPPERS_XP = "//div[contains(@class, 'product-row-wrapper')]";
        internal const string ALL_PROD_VARIANTS_XP = "div[id^='variant_']";
        internal const string PROD_VAR_QTYS_XP = "//div[contains(@id, 'variant')]/div[3]/span/span/input[2]";
        internal const string ROW_TITLE_XP = "//tbody/tr/td[2]/div/a[1]";
        internal const string ALL_QTY_BOXES_XP = "//input[contains(@class,'k-formatted-value')]";
        internal const string ROW_QTY_UP_XP = "//span[contains(@class,'k-i-arrow-n')]";
        internal const string ROW_QTY_DOWN_XP = "//span[contains(@class,'k-i-arrow-s')]";
        internal const string ROW_PRICE_XP = "//[contains(@class, 'price') and contains(@class ,'Hover')]";
        internal const string ROW_UPDATE_XP = "//tbody/tr/td[2]/div[2]/div[2]/button";

        /// <summary>cart page row identifiers</summary>
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


        /// <summary>purchase order locators</summary>
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

        /// <summary>mailbox IDs/locators</summary>
        internal const string STATUS_DD_ID = "processStatusList";
        internal const string PERIOD_DD_ID = "yearsList";
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

        /// <summary>supplier home page</summary>
        internal const string ORDER_FULFILLMENT = "Order Fulfillment";
        internal const string VIEW_POS = "View POs";
    }
}
