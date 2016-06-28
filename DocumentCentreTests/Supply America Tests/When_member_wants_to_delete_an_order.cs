using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;

namespace DocumentCentreTests.Supply_America_Tests
{    
    public class When_member_wants_to_delete_an_order : BaseDriverTest
    {
        //static ViewOrdersPage _voPage;
        static MemberHomePage _memHomePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;
        static HomePage _homePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Order Delete Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.SA_MEMBER_USER, Constants.SA_MEMBER_PASS);
            //_voPage = _homePage.NavigateToOrders("View Draft Orders");
            //_voPage.OrderType = Constants.ORDER_SEARCH_DRAFT;
            //_voPage.InitiateSearch();
            //_voPage.CheckFirstRow();
            _catPage = _memHomePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            _cartPage = _prodPage.NavigateToCart();
        };

        Because of = () => _memHomePage = _cartPage.DeleteOrder();

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
