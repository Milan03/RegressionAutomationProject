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

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{  
    public class When_SA_member_searches_for_catalogue : BaseDriverTest
    {
        static MemberHomePage _homePage;
        static CataloguesPage _catPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Search for Catalogue Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
        };

        Because of = () =>
        {
            _catPage = _homePage.NavigateToCatalogues();
            _catPage.InputCatalogueName("milan");
            _catPage.InitiateSearch();
            Thread.Sleep(1000);
        };

        It should_search_for_the_specified_catalogue = () =>
        {
            string catText = _driver.FindElement(By.XPath(Constants.PO.XP.CAT_LOCATOR)).Text;
            if (catText.Equals(Constants.Text.TEST_CAT)) { 
                _logger.Info("-- Member Search for Catalogue Test: [PASSED] --");
                catText.ShouldEqual(Constants.Text.TEST_CAT);
            }
            else
            {
                _logger.Fatal("-- Member Search for Catalogue Test: [FAILED] --");
                catText.ShouldEqual(Constants.Text.TEST_CAT);
            }
        };
    }
}
