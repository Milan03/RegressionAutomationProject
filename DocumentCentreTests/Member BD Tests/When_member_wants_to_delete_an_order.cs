﻿using DocumentCentreTests.Pages;
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
    public class When_member_wants_to_delete_an_order : BaseDriverTest
    {
        static ViewOrdersPage _voPage;
        static HomePage _homePage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Order Delete Test Initiating --");
            LoginPage loginPage = new LoginPage(_driver, "member");
            _homePage = loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS);
            _voPage = _homePage.NavigateToOrders("View Draft Orders");
            _voPage.OrderType = Constants.ORDER_SEARCH_DRAFT;
            _voPage.InitiateSearch();
            _voPage.CheckFirstRow();
        };

        Because of = () => _voPage.DeleteOrder();

        It should_delete_the_order = () =>
        {
            if (_voPage.AlertSuccess.Equals(true))
            {
                _logger.Info("-- Member Order Delete Test: [PASSED] --");
                _voPage.AlertSuccess.ShouldEqual(true);
            }
            else
            {
                _logger.Fatal("-- Member Order Delete Test: [FAILED] --");
                _voPage.AlertSuccess.ShouldEqual(false);
            }
        };
    }
}
