using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    [Timeout(100000)]
    
    public class When_member_navigates_to_PO_mailbox : BaseDriverTest
    {
        private static SupplierHomePage _suppHomepage;
        private static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Navigation Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
        };

        Because of = () => 
        {
            try
            {
                _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Navigation Test: [FAILED] --");
                _poInboxPage.PageReached.ShouldBeTrue();
            }
        };

        It should_display_the_po_mailbox = () =>
        {
            if (_poInboxPage.PageReached)
                _logger.Info("-- Member PO Mailbox Navigation Test: [SUCCESS] --");
        };
    }
}
