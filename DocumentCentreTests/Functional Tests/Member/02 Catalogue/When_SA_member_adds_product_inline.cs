using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(500000)]
    public class When_SA_member_adds_product_inline : BaseDriverTest
    {
        private static MemberHomePage _homePage;
        private static CataloguesPage _catPage;
        private static ProductsPage _prodPage;
        private static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Add Item Inline to Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            _cartPage = _prodPage.NavigateToCart();
        };

        Because of = () =>
        {
            try
            {
                _cartPage.AddItemInline("IIN-MILANTEST-05", Constants.Affiliation.SA.USER);
            }
            catch (System.Exception)
            {
                _logger.Fatal("-- Member Add Item Inline to Cart Test: [FAILED] --");
                _cartPage.AlertSuccess.ShouldBeTrue();
            }
        };

        It should_add_item_to_cart = () => 
        {
            if (_cartPage.AlertSuccess)
                _logger.Fatal("-- Member Add Item Inline to Cart Test: [PASSED] --");
        };
    }
}
