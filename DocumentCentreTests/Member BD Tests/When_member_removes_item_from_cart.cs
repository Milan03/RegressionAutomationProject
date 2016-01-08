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
            _cartPage = _prodPage.NavigateToCart();
            _cartPage.RemoveItemFromCart();
        };

        It should_return_alert_of_sueccess = () =>
        {
            if (!_cartPage.ItemDeleted)
            {
                _logger.Fatal("-- Member Remove Item From Cart Test: [FAILED] --");
                _cartPage.ItemDeleted.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member Remove Item From Cart Test: [PASSED] --");
                _cartPage.ItemDeleted.ShouldBeTrue();
            }
        };
    }
}
