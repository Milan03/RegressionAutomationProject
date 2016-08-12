using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(500000)]
    public class When_SA_member_adds_product_inline : BaseDriverTest
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
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            _cartPage = _prodPage.NavigateToCart();
        };

        Because of = () =>
        {
            _cartPage.AddItemInline("IIN-MILANTEST-05", Constants.Affiliation.SA.USER);
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
