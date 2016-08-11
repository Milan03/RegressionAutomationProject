using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_navs_to_next_page_of_grid : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Nav Next Page Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () =>
        {
            _poInboxPage.NavToNextPage();
        };

        It should_go_to_the_next_page_of_grid = () =>
        {
            if (!_poInboxPage.NavRowSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Nav Next Page Test: [FAILED] --");
                _poInboxPage.NavRowSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Nav Next Page Test: [PASSED] --");
                _poInboxPage.NavRowSuccess.ShouldBeTrue();
            }
        };
    }
}
