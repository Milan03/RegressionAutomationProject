using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using System.Threading;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_clicks_PObox_basic_search : BaseDriverTest
    {
        private static SupplierHomePage _suppHomepage;
        private static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Load Basic Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
        };

        Because of = () =>
        {
            try
            {
                _poInboxPage.LoadAdvancedSearch();
                Thread.Sleep(500);
                _poInboxPage.LoadBasicSearch();
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Load Basic Search Test: [FAILED] --");
                _poInboxPage.BasicLoadSuccess.ShouldBeTrue();
            }
        };

        It should_load_basic_search = () =>
        {
            if (_poInboxPage.BasicLoadSuccess)
                _logger.Info("-- Member PO Mailbox Load Basic Search Test: [PASSED] --");
        };
    }
}
