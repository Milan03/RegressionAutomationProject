using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(900000)]
    public class When_Drake_wants_to_edit_draft_po : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static ViewOrdersPage _voPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Edit Draft PO Test --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.Drake.MEMBER_USER, Constants.Affiliation.Drake.MEMBER_PASS);
            _voPage = _homePage.NavigateToOrders(Constants.Text.VIEW_DRAFT_ORDERS, Constants.OrderStatus.DRAFT);
        };

        Because of = () =>
        {
            try
            {
                _cartPage = _voPage.EditOrder(Constants.Text.ORDER_PO_DRAFT);
                _cartPage.AddItemInline("EE100", Constants.Affiliation.Drake.USER);
                _cartPage.SaveDraftOrder();
            }
            catch (System.Exception e)
            {
                _logger.Info("-- Drake Edit Draft PO Test: [FAILED] --");
            }
        };

        It should_save_the_edit = () =>
        {
            if (_cartPage.SaveDraftSuccess)
            {
                _logger.Info("-- Drake Edit Draft PO Test: [PASSED] --");
                _cartPage.SaveDraftSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Drake Edit Draft PO Test: [FAILED] --");
                _cartPage.SaveDraftSuccess.ShouldBeTrue();
            }
        };
    }
}
