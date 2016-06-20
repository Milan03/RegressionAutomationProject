using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Member_BD_Tests
{
    [Timeout(500000)]
    [Subject(typeof(LoginPage))]
    class When_members_adds_product_inline : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Add Item to Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.SA_PORTAL_USER, Constants.SA_PORTAL_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            //_prodPage.LoadProductRows();
            //_prodPage.AddItemToCart("IN-MILANTEST-01", 1);
            _cartPage = _prodPage.NavigateToCart();
            //_cartPage.LoadItemsInCart();
        };

        Because of = () =>
        {
            _cartPage.AddItemInline(_prodPage._prodsInCart, "IN-MILANTEST-05", 3);
        };

        It should_add_item_to_cart = () => 
        {

        };
    }
}
