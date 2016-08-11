using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Mailbox
{
    public class When_member_sets_period_dropdown : BaseDriverTest
    {
        static SupplierHomePage _suppHomepage;
        static POInboxPage _poInboxPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member PO Mailbox Set Period Dropdown Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "supplier");
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.SA_SUPPLIER_USER, Constants.SA_SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.VIEW_POS);
        };

        Because of = () => _poInboxPage.SetPeriodDropdown(Constants.PeriodYear.Last90);

        It should_set_the_period = () =>
        {
            if (!_poInboxPage.PeriodSetSuccess)
            {
                _logger.Fatal("-- Member PO Mailbox Set Period Dropdown Test: [FAILED] --");
                _poInboxPage.PeriodSetSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member PO Mailbox Set Period Dropdown Test: [PASSED] --");
                _poInboxPage.PeriodSetSuccess.ShouldBeTrue();
            }
        };
    }
}
