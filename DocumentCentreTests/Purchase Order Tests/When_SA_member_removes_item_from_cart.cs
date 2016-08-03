using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using System.Threading;

namespace DocumentCentreTests.Purchase_Order_Tests
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
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.SA_MEMBER_USER, Constants.SA_MEMBER_PASS);
            _voPage = _homePage.NavigateToOrders("View Draft Orders");
            _cartPage = _voPage.ReCreateOrder("AC-0000238");
            _cartPage.LoadItemsInCart();
        };
        
        Because of = () =>
        {
            _cartPage.RemoveItemFromCart("IN-MILANTEST-02");
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
