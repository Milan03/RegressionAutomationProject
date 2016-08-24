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
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
        };

        Because of = () =>
        {
            try
            {
                _poInboxPage.LoadGridRows();
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Load Grid Test: [FAILED] --");
                _poInboxPage.GridLoadSuccess.ShouldBeTrue();
            }
        };

        It should_load_all_grid_elements = () =>
        {
            if (_poInboxPage.GridLoadSuccess)
                _logger.Info("-- Member PO Mailbox Load Grid Test: [SUCCESS] --");

        };
    }
}
