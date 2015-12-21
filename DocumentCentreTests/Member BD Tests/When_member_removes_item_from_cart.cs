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
        static Exception _navException;

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
            _navException = Catch.Exception(() => _prodPage.NavigateToCart()); 

        };

        It should_return_alert_of_sueccess = () =>
        {
            _navException.ShouldBeNull();
        };
    }
}
