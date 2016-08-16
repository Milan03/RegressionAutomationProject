using DocumentCentreTests.Models;
using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using OpenQA.Selenium;
using System;
using System.Runtime.CompilerServices;

namespace DocumentCentreTests.Functional_Tests.Member.Login
{
    public class When_member_logs_in_with_invalid_creds : BaseDriverTest
    {
        static LoginPage _loginPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Invalid Login Test Initiating --");
            _loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
        };

        Because of = () => _loginPage.SubmitLoginExpectingFailure();

        It should_have_failed_to_log_in = () =>
        {
            var error = HelperMethods.FindElement(_driver, Constants.SearchType.CLASSNAME, "login-error-message");
            if (error.Text.Equals(Constants.UIMessages.LOGIN_ERROR))
            {
                _logger.Info("-- Member Invliad Login Test: [PASSED] --");
                error.Text.ShouldEqual(Constants.UIMessages.LOGIN_ERROR);
                _loginPage.LoginSuccess.ShouldBeFalse();
            }
            else
            {
                _logger.Fatal("-- Member Invalid Login Test: [FAILED] --");
                error.Text.ShouldEqual(Constants.UIMessages.LOGIN_ERROR);
                _loginPage.LoginSuccess.ShouldBeFalse();
            }
        };
    }
}
