using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_navs_to_last_page_of_grid : BaseDriverTest
    {
        private static SupplierHomePage _suppHomepage;
        private static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Nav Previous Page Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
        };

        Because of = () =>
        {
            try
            {
                _poInboxPage.NavToLastPage();
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Nav Last Page Test: [FAILED] --");
                _poInboxPage.NavRowSuccess.ShouldBeTrue();
            }
        };

        It should_go_to_the_last_page_of_grid = () =>
        {
            if (_poInboxPage.NavRowSuccess)
                _logger.Info("-- Member PO Mailbox Nav Last Page Test: [FAILED] --");
        };
    }
}
