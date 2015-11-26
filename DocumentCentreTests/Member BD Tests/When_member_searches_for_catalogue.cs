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
    public class When_member_searches_for_catalogue : BaseDriverTest
    {
        static HomePage _homePage;
        static CataloguesPage _catPage;
        static Exception _navigationExcep;
        static Exception _inputException;
        static Exception _searchException;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Search for Catalogue Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
        };

        Because of = () =>
        {
            _navigationExcep = Catch.Exception(() => _catPage = _homePage.NavigateToCatalogues());
            _inputException = Catch.Exception(() => _catPage.InputCatalogueName(Constants.SA_TEST_CAT));
            _searchException = Catch.Exception(() => _catPage.InitiateSearch());
            Thread.Sleep(1000);
        };

        It should_search_for_a_catalogue = () =>
        {
            string catText = _driver.FindElement(By.XPath(Constants.XPATH_CAT_LOCATOR)).Text;
            if (catText.Equals(Constants.SA_TEST_CAT))
                _logger.Info("-- Member Search for Catalogue Test: [PASSED] --");
            else
            {
                _logger.Fatal("-- Member Search for Catalogue Test: [FAILED] --");
                _inputException.ShouldBeNull();
                _searchException.ShouldBeNull();
                catText.ShouldEqual(Constants.SA_TEST_CAT);
            }
        };
    }
}
