using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{ 
    public class When_SA_member_searches_for_an_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static MemberHomePage _homePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Valid Order Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
        };

        Because of = () =>
        {
            _voPage = _homePage.NavigateToOrders(Constants.Text.VIEW_ORDERS, Constants.OrderStatus.ALL);
            _voPage.InputPurchaseOrder(Constants.Text.ORDER_PO_PROC);
            _voPage.ChooseOrderType(Constants.OrderStatus.PROCESSING);
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };


        It should_have_searched_for_an_order = () =>
        {
            if (_voPage.FirstTableElem.Text.Equals(Constants.Text.ORDER_PO_PROC))
            {
                _logger.Info("-- Member Valid Order Search Test: [PASSED] --");
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.Text.ORDER_PO_PROC);
            }
            else
            {
                _logger.Fatal("-- Member Valid Order Search Test: [FAILED] --");
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.Text.ORDER_PO_PROC);
            }
        };
    }
}
