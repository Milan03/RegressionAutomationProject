using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests
{
    [Subject(typeof(LoginPage))]
    public class When_member_searches_for_an_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static HomePage _homePage;
        static Exception _inputException;
        static Exception _searchException;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Search Order Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);           
        };

        Because of = () =>
           {
               _voPage = _homePage.NavigateToViewOrders();
               _inputException = Catch.Exception(() => _voPage.InputPurchaseOrder(Constants.ORDER_PO_PROC));
               _voPage.ChooseOrderType(Constants.ORDER_SEARCH_PROC);
               _searchException = Catch.Exception(() => _voPage.Search());
           };


        It should_have_searched_for_an_order = () =>
            {
                _inputException.ShouldBeNull();
                _searchException.ShouldBeNull();
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.ORDER_PO_PROC);
            };
    }
}
