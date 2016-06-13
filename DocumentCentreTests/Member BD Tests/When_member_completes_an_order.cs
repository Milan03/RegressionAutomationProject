using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using NCrunch.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentCentreTests.Member_BD_Tests
{
    [Timeout(100000)]
    [Subject(typeof(LoginPage))]
    public class When_member_completes_an_order : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Purchase Order Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            _prodPage.LoadProductRows();
            _prodPage.AddItemToCart("IN-MILANTEST-05", 1);
        };

        Because of = () =>
        {
            _cartPage = _prodPage.NavigateToCart();
            _cartPage.SendOrder();
        };

        It should_return_alert_of_success = () =>
        {
            if (!_cartPage.OrderComplete)
            {
                _logger.Fatal("-- Member Purchase Order Test: [FAILED] --");
                _cartPage.OrderComplete.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member Purchase Order Test: [PASSED] --");
                _cartPage.OrderComplete.ShouldBeTrue();
            }
        };
    }
}
