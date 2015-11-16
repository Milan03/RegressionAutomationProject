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
    public class After_member_searches_for_an_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static HomePage _homePage;

        Establish context = () =>
        {
            LoadDriver();
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);           
        };

        Because of = () =>
           {
               _voPage = _homePage.NavigateToViewOrders();
           };


        It should_have_searched_for_an_order = () =>
            {
                Catch.Exception(() => _voPage.InputPurchaseOrder(Constants.ORDER_PO_PROC)).ShouldBeNull();
                Catch.Exception(() => _voPage.SearchOrders(Constants.ORDER_SEARCH_PROC));
            };
    }
}
