using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Purchase_Order_Tests
{ 
    public class When_SA_member_searches_for_an_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static MemberHomePage _homePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Valid Order Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.SA_MEMBER_USER, Constants.SA_MEMBER_PASS);
        };

        Because of = () =>
        {
            _voPage = _homePage.NavigateToOrders("View Orders");
            _voPage.InputPurchaseOrder(Constants.ORDER_PO_PROC);
            _voPage.ChooseOrderType(Constants.ORDER_SEARCH_PROC);
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };


        It should_have_searched_for_an_order = () =>
        {
            if (_voPage.FirstTableElem.Text.Equals(Constants.ORDER_PO_PROC))
            {
                _logger.Info("-- Member Valid Order Search Test: [PASSED] --");
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.ORDER_PO_PROC);
            }
            else
            {
                _logger.Fatal("-- Member Valid Order Search Test: [FAILED] --");
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.ORDER_PO_PROC);
            }
        };
    }
}
