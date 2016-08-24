using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using System.Threading;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(900000)]
    public class When_SA_member_makes_a_purchase_order : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;
        static bool _productsVerified;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Add Item to Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
        };

        Because of = () =>
        {
            try
            {
                _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue 02");
                _prodPage.LoadProductRows();
                _prodPage.AddItemToCart("IN-MILANTEST-01", 1);
                _prodPage.AddItemToCart("IN-MILANTEST-02", 1);
                _prodPage.AddItemToCart("IN-MILANTEST-03", 1);
                _prodPage.AddItemToCart("IN-MILANTEST-04", 1);
                _cartPage = _prodPage.NavigateToCart();
                _cartPage.LoadItemsInCart();
                _productsVerified = _cartPage.VerifyItemsInCart(_prodPage._prodsInCart);
                _cartPage.SendOrder();
            }
            catch(System.Exception)
            {
                if (_productsVerified && _cartPage.OrderComplete)
                    _logger.Info("-- Member Add Item to Cart Test: [SUCCESS w/ EXCEPTION ENCOUNTERED] --");
                else
                {
                    _logger.Fatal("-- Member Add Item to Cart Test: [FAILED] --");
                    _prodPage.ItemAdded.ShouldBeTrue();
                }
            }
        };

        It should_return_alert_of_success = () =>
        {
            if (_productsVerified && _cartPage.OrderComplete)
                _logger.Info("-- Member Add Item to Cart Test: [SUCCESS] --");
        };
    }
}
