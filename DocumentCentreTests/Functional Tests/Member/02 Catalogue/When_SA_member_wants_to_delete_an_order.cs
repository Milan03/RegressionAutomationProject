using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_SA_member_wants_to_delete_an_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static MemberHomePage _memHomePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Order Delete Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _memHomePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _voPage = _memHomePage.NavigateToOrders("View Draft Orders", "Draft");
            _voPage.OrderType = Constants.OrderStatus.DRAFT;
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };

        Because of = () => _voPage.DeleteOrder();

        It should_delete_the_order = () =>
        {
            if (_memHomePage.PageReached.Equals(true))
            {
                _logger.Info("-- Member Order Delete Test: [PASSED] --");
                _memHomePage.PageReached.ShouldEqual(true);
            }
            else
            {
                _logger.Fatal("-- Member Order Delete Test: [FAILED] --");
                _memHomePage.PageReached.ShouldEqual(false);
            }
        };
    }
}
