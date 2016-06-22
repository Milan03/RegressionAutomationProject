using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Mailbox_Tests
{
    [Timeout(100000)]
    [Subject(typeof(LoginPage))]
    public class When_member_navigates_to_PO_mailbox : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Navigation Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
           
        };

        Because of = () => _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);

        It should_display_the_po_mailbox = () =>
        {
            if (!_poInboxPage.PageReached)
            {
                _logger.Fatal("-- Member PO Mailbox Navigation Test: [FAILED] --");
                _poInboxPage.PageReached.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Navigation Test: [PASSED] --");
                _poInboxPage.PageReached.ShouldBeTrue();
            }
        };
    }
}
