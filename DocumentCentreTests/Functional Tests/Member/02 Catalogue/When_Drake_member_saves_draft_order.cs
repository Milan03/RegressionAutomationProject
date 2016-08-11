using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;

namespace DocumentCentreTests.Purchase_Order_Tests
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
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.DRAKE_MEMBER_USER, Constants.DRAKE_MEMBER_PASS);
            _cataPage = _homePage.NavigateToCatalogues();
            _cataPage.InputCatalogueName("Knauf Insulation");
            _cataPage.InitiateSearch();
            _cartPage = _cataPage.ChooseDrakeCatalogue("Knauf Insulation Copy");
            _cartPage.AddItemInline("EE100", Constants.DRAKE_USER);
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
