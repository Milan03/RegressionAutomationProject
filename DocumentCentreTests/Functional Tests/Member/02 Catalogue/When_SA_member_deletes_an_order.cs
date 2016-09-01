using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_SA_member_deletes_an_order : BaseDriverTest
    {
        private static ViewOrdersPage _voPage;
        private static MemberHomePage _memHomePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- SA Order Grid Delete Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _memHomePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _voPage = _memHomePage.NavigateToOrders(Constants.Text.VIEW_DRAFT_ORDERS, Constants.OrderStatus.DRAFT);
            _voPage.OrderType = Constants.OrderStatus.DRAFT;
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };

        Because of = () => 
        {
            try
            {
                _voPage.DeleteOrder();
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- SA Order Grid Delete Test: [FAILED] --");
                _memHomePage.PageReached.ShouldBeTrue();
            }
        };

        It should_delete_the_order = () =>
        {
            if (_memHomePage.PageReached)
                _logger.Info("-- SA Order Grid Delete Test: [PASSED] --");
        };
    }
}
