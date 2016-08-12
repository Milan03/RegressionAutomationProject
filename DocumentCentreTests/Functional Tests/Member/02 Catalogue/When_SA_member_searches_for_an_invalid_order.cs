using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentCentreTests.Functional_Tests.Member.Catalogue
{
    public class When_SA_member_searches_for_an_invalid_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static MemberHomePage _homePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Invalid Order Search Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
            _homePage = (MemberHomePage)loginPage.LoginAs(Constants.Affiliation.SA.MEMBER_USER, Constants.Affiliation.SA.MEMBER_PASS);
        };

        Because of = () =>
        {
            _voPage = _homePage.NavigateToOrders(Constants.Text.VIEW_ORDERS, Constants.OrderStatus.ALL);
            _voPage.InputPurchaseOrder(Constants.Text.INVALID_PO);
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };


        It should_show_no_orders_found = () =>
        {
            if (_voPage.FirstTableElem.Text.Equals(Constants.UIMessages.ORDER_ERROR))
            {
                _logger.Info("-- Member Invalid Order Search Test: [PASSED] --");
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.UIMessages.ORDER_ERROR);
            }
            else
            {
                _logger.Fatal("-- Member Invalid Order Search Test: [FAILED] --");
                _voPage.FirstTableElem.Text.ShouldEqual(Constants.UIMessages.ORDER_ERROR);
            }
        };
    }
}
