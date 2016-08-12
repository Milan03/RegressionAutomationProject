using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using System.Threading;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(100000)]
    public class When_SA_member_removes_item_from_cart : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static ViewOrdersPage _voPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Remove Item From Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _voPage = _homePage.NavigateToOrders(Constants.Text.VIEW_DRAFT_ORDERS, Constants.OrderStatus.DRAFT);
            _cartPage = _voPage.ReCreateOrder("RE01");
            _cartPage.LoadItemsInCart();
        };
        
        Because of = () =>
        {
            _cartPage.RemoveItemFromCart("IN-MILANTEST-04");
        };

        It should_return_alert_of_sueccess = () =>
        {
            if (!_cartPage.ItemDeleted)
            {
                _logger.Fatal("-- Member Remove Item From Cart Test: [FAILED] --");
                _cartPage.ItemDeleted.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member Remove Item From Cart Test: [PASSED] --");
                _cartPage.ItemDeleted.ShouldBeTrue();
            }
        };
    }
}
