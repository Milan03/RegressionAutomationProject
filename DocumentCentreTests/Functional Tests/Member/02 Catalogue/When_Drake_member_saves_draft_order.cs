using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_Drake_member_saves_draft_order : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static CataloguesPage _cataPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Drake Draft Order Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.Drake.MEMBER_USER, Constants.Affiliation.Drake.MEMBER_PASS);
            _cataPage = _homePage.NavigateToCatalogues();
            _cataPage.InputCatalogueName("Knauf Insulation");
            _cataPage.InitiateSearch();
            _cartPage = _cataPage.ChooseDrakeCatalogue("Knauf Insulation");
            _cartPage.AddItemInline("EE100", Constants.Affiliation.Drake.USER);
        };

        Because of = () =>
        {
            _cartPage.SaveDraftOrder();
        };

        It should_save_the_order = () =>
        {
            if (_cartPage.SaveDraftSuccess)
            {
                _logger.Info("-- Drake Draft Order Test: [PASSED] --");
                _cartPage.SaveDraftSuccess.ShouldBeTrue();
            }
            else
            {
                _logger.Fatal("-- Drake Draft Order Test: [FAILED] --");
                _cartPage.SaveDraftSuccess.ShouldBeTrue();
            }
        };
    }
}
