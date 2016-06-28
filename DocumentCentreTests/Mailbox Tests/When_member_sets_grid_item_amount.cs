using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Mailbox_Tests
{
    public class When_member_sets_grid_item_amount : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Set Grid Item Amount Dropdown Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () =>
        {
            _poInboxPage.SetGridItemAmount(Constants.GridElementsToDisplay.Ten);
        };

        It should_go_to_the_last_page_of_grid = () =>
        {
            if (!_poInboxPage.GridAmountSetSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Set Grid Item Amount Dropdown Test: [FAILED] --");
                _poInboxPage.GridAmountSetSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Set Grid Item Amount Dropdown Test: [PASSED] --");
                _poInboxPage.GridAmountSetSuccess.ShouldBeTrue();
            }
        };
    }
}
