using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_loads_PObox_grid : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Load Grid Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () =>
        {
            _poInboxPage.LoadGridRows();
        };

        It should_load_all_grid_elements = () =>
        {
            if (!_poInboxPage.GridLoadSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Load Grid Test: [FAILED] --");
                _poInboxPage.GridLoadSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Load Grid Test: [PASSED] --");
                _poInboxPage.GridLoadSuccess.ShouldBeTrue();
            }
        };
    }
}
