using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_sets_doc_search_status : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Set Status Dropdown Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () => _poInboxPage.SetSearchStatus(Constants.SearchStatus.Unprocessed);

        It should_set_the_status = () =>
        {
            if (!_poInboxPage.StatusSetSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Set Status Dropdown Test: [FAILED] --");
                _poInboxPage.StatusSetSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Set Status Dropdown Test: [PASSED] --");
                _poInboxPage.StatusSetSuccess.ShouldBeTrue();
            }
        };
    }
}
