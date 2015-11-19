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

namespace DocumentCentreTests
{
    namespace MemberPortalTests
    {
        [Subject(typeof(LoginPage))]
        public class When_member_wants_to_delete_an_order : BaseDriverTest
        {
            static ViewOrdersPage _voPage;
            static HomePage _homePage;
            static Exception _searchException;

            Establish context = () =>
            {
                LoadDriver();
                _logger.Info("-- Member Order Delete Test Initiating --");
                LoginPage loginPage = new LoginPage(_driver, "member");
                _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
                _voPage = _homePage.NavigateToViewOrders();
                _voPage.ChooseOrderType(Constants.ORDER_SEARCH_DRAFT);
                _searchException = Catch.Exception(() => _voPage.Search());
                _voPage.CheckFirstRow();
            };

            Because of = () =>
               {
                   _voPage.DeleteOrder();
               };


            It should_delete_the_order = () =>
                {
                    _searchException.ShouldBeNull();
                    
                    if (_voPage.FirstTableElem.Text.Equals(Constants.ORDER_PO_PROC))
                        _logger.Info("-- Member Order Delete Test: [PASSED] --");
                    else
                    {
                        _logger.Fatal("-- Member Order Delete Test: [FAILED] --");
                        _voPage.FirstTableElem.Text.ShouldEqual(Constants.ORDER_PO_PROC);
                    }
                };
        }
    }
}
