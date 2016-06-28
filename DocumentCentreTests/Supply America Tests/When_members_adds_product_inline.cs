using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Supply_America_Tests
{
    [Timeout(500000)]
    class When_members_adds_product_inline : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Add Item Inline to Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.SA_MEMBER_USER, Constants.SA_MEMBER_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            _cartPage = _prodPage.NavigateToCart();
        };

        Because of = () =>
        {
            _cartPage.AddItemInline("IN-MILANTEST-05");
        };

        It should_add_item_to_cart = () => 
        {
            if (_cartPage.AlertSuccess.Equals(true))
            {
                _logger.Fatal("-- Member Add Item Inline to Cart Test: [PASSED] --");
                _cartPage.AlertSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Fatal("-- Member Add Item Inline to Cart Test: [FAILED] --");
                _cartPage.AlertSuccess.ShouldBeTrue();
            }
        };
    }
}
