using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using System.Threading;

namespace DocumentCentreTests.Mailbox_Tests
{
    [Subject(typeof(LoginPage))]
    public class When_member_clicks_basic_search : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Load Basic Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () =>
        {
            _poInboxPage.LoadAdvancedSearch();
            Thread.Sleep(500);
            _poInboxPage.LoadBasicSearch();
        };

        It should_load_basic_search = () =>
        {
            if (!_poInboxPage.BasicLoadSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Load Basic Search Test: [FAILED] --");
                _poInboxPage.BasicLoadSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Load Basic Search Test: [PASSED] --");
                _poInboxPage.BasicLoadSuccess.ShouldBeTrue();
            }
        };
    }
}
