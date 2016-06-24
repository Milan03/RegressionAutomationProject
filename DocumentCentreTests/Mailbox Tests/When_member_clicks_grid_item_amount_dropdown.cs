using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Mailbox_Tests
{
    public class When_member_clicks_grid_item_amount_dropdown : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Load Grid Item Amount Dropdown Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () =>
        {
            _poInboxPage.LoadGridItemAmountDropdown();
        };

        It should_set_that_amount_of_items_to_display = () =>
        {
            if (!_poInboxPage.GridAmountDropdownSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Load Grid Item Amount Dropdown Test: [FAILED] --");
                _poInboxPage.GridAmountDropdownSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Load Grid Item Amount Dropdown Test: [PASSED] --");
                _poInboxPage.GridAmountDropdownSuccess.ShouldBeTrue();
            }
        };
    }
}
