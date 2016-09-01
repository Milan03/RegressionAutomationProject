using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_sets_grid_item_amount : BaseDriverTest
    {
        private static SupplierHomePage _suppHomepage;
        private static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Set Grid Item Amount Dropdown Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
        };

        Because of = () =>
        {
            try
            {
                _poInboxPage.SetGridItemAmount(Constants.BaseMailbox.Enums.GridElementsToDisplay.TEN);
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Set Grid Item Amount Dropdown Test: [FAILED] --");
                _poInboxPage.GridAmountSetSuccess.ShouldBeTrue();
            }
        };

        It should_go_to_the_last_page_of_grid = () =>
        {
            if (_poInboxPage.GridAmountSetSuccess)
                _logger.Info("-- Member PO Mailbox Set Grid Item Amount Dropdown Test: [FAILED] --");
        };
    }
}
