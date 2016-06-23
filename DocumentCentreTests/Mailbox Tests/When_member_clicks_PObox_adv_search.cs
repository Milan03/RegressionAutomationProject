using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using System.Threading;

namespace DocumentCentreTests.Mailbox_Tests
{
    [Timeout(100000)]
    [Subject(typeof(LoginPage))]
    public class When_member_clicks_PObox_adv_search : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Load Adv Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () =>
        {
            _poInboxPage.LoadAdvancedSearch();
        };

        It should_load_advanced_search = () =>
        {
            if (!_poInboxPage.AdvLoadSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Load Adv Search Test: [FAILED] --");
                _poInboxPage.AdvLoadSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Load Adv Search Test: [PASSED] --");
                _poInboxPage.AdvLoadSuccess.ShouldBeTrue();
            }
        };
    }
}

