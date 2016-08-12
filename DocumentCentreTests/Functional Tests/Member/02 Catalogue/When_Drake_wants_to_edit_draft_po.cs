using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member._02_Catalogue
{
    public class When_Drake_wants_to_edit_draft_po : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static ViewOrdersPage _voPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Member Edit Draft PO Test --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.Drake.MEMBER_USER, Constants.Affiliation.Drake.MEMBER_PASS);
        };

        Because of = () =>
        {
            _voPage = _homePage.NavigateToOrders(Constants.Text.VIEW_DRAFT_ORDERS, Constants.OrderStatus.DRAFT);
        };

        It should_save_the_edit = () =>
        {

        };
    }
}
