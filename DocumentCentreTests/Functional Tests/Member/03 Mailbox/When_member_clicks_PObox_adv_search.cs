using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using System.Threading;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    [Timeout(100000)]
    
    public class When_member_clicks_PObox_adv_search : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Load Adv Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.SUPPLIER);
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
        };

        Because of = () =>
        {
            try
            {
                _poInboxPage.LoadAdvancedSearch();
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Load Adv Search Test: [FAILED] --");
            }
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

