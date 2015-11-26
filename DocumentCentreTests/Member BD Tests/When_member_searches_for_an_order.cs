using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Member_BD_Tests
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
            _logger.Info("-- Member Valid Order Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
        };

        Because of = () =>
        {
            _voPage = _homePage.NavigateToOrders("View Orders");
            _inputException = Catch.Exception(() => _voPage.InputPurchaseOrder(Constants.ORDER_PO_PROC));
            _voPage.ChooseOrderType(Constants.ORDER_SEARCH_PROC);
            _searchException = Catch.Exception(() => _voPage.InitiateSearch());
            _voPage.CheckFirstRow();
        };


        It should_have_searched_for_an_order = () =>
        {
            if (_voPage.FirstTableElem.Text.Equals(Constants.ORDER_PO_PROC))
                _logger.Info("-- Member Valid Order Search Test: [PASSED] --");
            else
            {
                _logger.Fatal("-- Member Valid Order Search Test: [FAILED] --");
                _inputException.ShouldBeNull();
                _searchException.ShouldBeNull();
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.ORDER_PO_PROC);
            }
        };
    }
}
