using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentCentreTests.Member_BD_Tests
{
    [Subject(typeof(LoginPage))]
    public class When_member_creates_draft_order : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;
        static Exception _catException;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Draft Order Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
        };

        Because of = () =>
        {
            _catException = Catch.Exception(() => _prodPage = _catPage.ChooseCatalogue(Constants.TEST_CAT));
            _cartPage = _prodPage.NavigateToCart();
        };

        It should_save_the_draft_order = () =>
        {

        };
    }
}
