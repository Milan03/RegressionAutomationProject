using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    [Timeout(900000)]
    public class When_Drake_member_makes_a_purchase_order : BaseDriverTest
    {
        private static MemberHomePage _homePage;
        private static CataloguesPage _cataPage;
        private static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Purchase Order Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.Drake.MEMBER_USER, Constants.Affiliation.Drake.MEMBER_PASS);
            _cataPage = _homePage.NavigateToCatalogues();
            _cataPage.InputCatalogueName("Knauf Insulation");
            _cataPage.InitiateSearch();
            _cartPage = _cataPage.ChooseDrakeCatalogue("Knauf Insulation Copy");
        };

        Because of = () =>
        {
            try
            {
                _cartPage.AddItemInline("EE100", Constants.Affiliation.Drake.USER);
                System.Threading.Thread.Sleep(500);
                _cartPage.AddItemInline("EE1669", Constants.Affiliation.Drake.USER);
                _cartPage.SendOrder();
            }
            catch(System.Exception)
            {
                _logger.Fatal("-- Drake PO Test: [FAILED] --");
                _cartPage.AlertSuccess.ShouldBeTrue();
            }
        };

        It should_complete_purchase_order = () =>
        {
            if (_cartPage.AlertSuccess.Equals(true))
                _logger.Info("-- Drake PO Test: [PASSED] --");
        };
    }
}
