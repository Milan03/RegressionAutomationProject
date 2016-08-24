using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_Drake_member_recreates_po : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static ViewOrdersPage _voPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Recreate PO Test --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.Drake.MEMBER_USER, Constants.Affiliation.Drake.MEMBER_PASS);
            _voPage = _homePage.NavigateToOrders(Constants.Text.VIEW_DRAFT_ORDERS, Constants.OrderStatus.DRAFT);
        };

        Because of = () =>
        {
            try
            {
                _voPage.ReCreateOrder(Constants.Text.ORDER_PO_RECREATE);
            }
            catch (System.Exception)
            {
                _logger.Info("-- Drake Recreate PO Test: [FAILED] --");
                _voPage.AlertSuccess.ShouldBeTrue();
            }
        };

        It should_recreate_the_order_in_MyCart = () =>
        {
            if(_voPage.AlertSuccess)
                _logger.Info("-- Drake Recreate PO Test: [PASSED] --");
        };
    }
}
