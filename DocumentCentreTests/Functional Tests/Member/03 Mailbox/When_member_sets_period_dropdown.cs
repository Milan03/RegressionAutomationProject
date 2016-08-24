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
            _suppHomepage = (SupplierHomePage)loginPage.LoginAs(Constants.Affiliation.SA.SUPPLIER_USER, Constants.Affiliation.SA.SUPPLIER_PASS);
            _poInboxPage = (POInboxPage)_suppHomepage.NavigateToMailbox(Constants.Text.VIEW_POS);
        };

        Because of = () => 
        {
            try
            {
                _poInboxPage.SetPeriodDropdown(Constants.BaseMailbox.Enums.PeriodYear.LAST90);
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Member PO Mailbox Set Period Dropdown Test: [FAILED] --");
                _poInboxPage.PeriodSetSuccess.ShouldBeTrue();
            }
        };

        It should_set_the_period = () =>
        {
            if (_poInboxPage.PeriodSetSuccess)
                _logger.Info("-- Member PO Mailbox Set Period Dropdown Test: [SUCCESS] --");
        };
    }
}
