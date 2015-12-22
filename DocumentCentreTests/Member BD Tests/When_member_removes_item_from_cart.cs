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
    public class When_member_removes_item_from_cart : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;

        static Exception _navException;
        static Exception _delException;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Remove Item From Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
        };

        Because of = () =>
        {
            _navException = Catch.Exception(() => _cartPage = _prodPage.NavigateToCart());
            _delException = Catch.Exception(() => _cartPage.RemoveItemFromCart("Reebok Ankle Height Hiking Shoe"));
        };

        It should_return_alert_of_sueccess = () =>
        {
            _navException.ShouldBeNull();
            _delException.ShouldBeNull();
            _cartPage.ItemDeleted.ShouldBeTrue();
        };
    }
}
