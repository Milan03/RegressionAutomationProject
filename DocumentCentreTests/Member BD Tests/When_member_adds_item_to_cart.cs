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
    public class When_member_adds_item_to_cart : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;
        static ProductsPage _prodPage;
        static MyCartPage _cartPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Add Item to Cart Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
        };

        Because of = () =>
        {
            _prodPage = _catPage.ChooseCatalogue("Milan Automation Catalogue");
            _prodPage.LoadProductRows();
            _prodPage.AddItemToCart("IN-MILANTEST-01", 1);
            _prodPage.AddItemToCart("IN-MILANTEST-02", 1);
            _prodPage.AddItemToCart("IN-MILANTEST-03", 1);
            //_prodPage.AddItemToCart("IN-MILANTEST-04", 1);
            _cartPage = _prodPage.NavigateToCart();
            _cartPage.LoadItemsInCart();
            _cartPage.VerifyItemsInCart(_prodPage._prodsInCart);

        };

        It should_return_alert_of_success = () =>
        {
            if (!_prodPage.ItemAdded)
            {
                _logger.Fatal("-- Member Add Item to Cart Test: [FAILED] --");
                _prodPage.ItemAdded.ShouldBeTrue();
            }
            else
            {
                _logger.Info("-- Member Add Item to Cart Test: [PASSED] --");
                _prodPage.ItemAdded.ShouldBeTrue();
            }
        };
    }
}
