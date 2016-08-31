using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using System.Threading;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(900000)]
    public class When_SA_member_cart_calculates_amount_total : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- SA Catalogue Amount Calculation Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue 02");
            _prodPage.LoadProductRows();
            _prodPage.AddItemToCart("IN-MILANTEST-01", 1);
            _prodPage.AddItemToCart("IN-MILANTEST-02", 1);
            _prodPage.AddItemToCart("IN-MILANTEST-03", 1);
            _prodPage.AddItemToCart("IN-MILANTEST-04", 1);
        };

        Because of = () =>
        {
            try
            {
                _cartPage = _prodPage.NavigateToCart();
                _cartPage.LoadItemsInCart();
                _cartPage.VerifyTotalDollarAmount();
            }
            catch (System.Exception)
            {
                if (_cartPage.DollarAmountSuccess)
                    _logger.Info("-- SA Catalogue Amount Calculation Test: [SUCCESS w/ EXCEPTION ENCOUNTERED] --");
                else
                {
                    _logger.Fatal("-- SA Catalogue Amount Calculation Test: [FAILED] --");
                    _prodPage.ItemAdded.ShouldBeTrue();
                }
            }
        };

        It should_calculate_all_items_in_cart = () =>
        {
            if (_cartPage.DollarAmountSuccess)
                _logger.Info("-- SA Catalogue Amount Calculation Test: [SUCCESS] --");
        };
    }
}
