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
    public class When_member_searches_for_an_invalid_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static HomePage _homePage;
        static Exception _inputException;
        static Exception _searchException;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Search Invliad Order Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);           
        };

        Because of = () =>
           {
               _voPage = _homePage.NavigateToViewOrders();
               _inputException = Catch.Exception(() => _voPage.InputPurchaseOrder(Constants.INVALID_PO));
               _searchException = Catch.Exception(() => _voPage.Search());
               _voPage.CheckFirstRow();
           };


        It should_show_no_orders_found = () =>
            {
                _inputException.ShouldBeNull();
                _searchException.ShouldBeNull();
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.ORDER_ERROR_MSG);
            };
    }
}
