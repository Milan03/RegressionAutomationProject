using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentCentreTests.Member_BD_Tests
{
    [Subject(typeof(LoginPage))]
    public class When_member_searches_for_catalogue : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Search for Catalogue Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
        };

        Because of = () =>
        {
            _catPage = _homePage.NavigateToCatalogues();
            Thread.Sleep(1000);
        };

        It should_search_for_a_catalogue = () =>
        {

        };
    }
}
