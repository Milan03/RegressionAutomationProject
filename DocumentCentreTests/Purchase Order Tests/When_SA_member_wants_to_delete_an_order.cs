using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Purchase_Order_Tests
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
            _memHomePage = (MemberHomePage)loginPage.LoginAs(Constants.SA_MEMBER_USER, Constants.SA_MEMBER_PASS);
            _voPage = _memHomePage.NavigateToOrders("View Draft Orders");
            _voPage.OrderType = Constants.ORDER_SEARCH_DRAFT;
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
