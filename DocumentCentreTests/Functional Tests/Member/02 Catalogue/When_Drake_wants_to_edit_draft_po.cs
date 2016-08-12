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
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.DRAKE_MEMBER_USER, Constants.DRAKE_MEMBER_PASS);
        };

        Because of = () =>
        {
            _voPage = _homePage.NavigateToOrders("View Draft Orders", "Draft");
        };

        It should_save_the_edit = () =>
        {

        };
    }
}
