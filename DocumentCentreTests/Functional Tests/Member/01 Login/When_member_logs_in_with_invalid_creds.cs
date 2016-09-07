using DocumentCentreTests.Pages;
using DocumentCentreTests.Util;
using Machine.Specifications;
using System;

namespace DocumentCentreTests.Functional_Tests.Member.Login
{
    public class When_member_logs_in_with_invalid_creds : BaseDriverTest
    {
        private static LoginPage _loginPage;

        Establish context = () =>
        {
            LoadDriver();
            _logger.Info("-- Member Invalid Login Test Initiating --");
            _loginPage = new LoginPage(_driver, Constants.UserType.MEMBER);
        };

        Because of = () => 
        {
            try
            {
                _loginPage.SubmitLoginExpectingFailure();
                //var error = HelperMethods.FindElement(_driver, Constants.SearchType.CLASSNAME, "login-error-message");
                if (!_loginPage.LoginSuccess)
                    _logger.Info("-- Member Invliad Login Test: [PASSED] --");
                else
                {
                    _logger.Fatal("-- Member Invalid Login Test: [FAILED] --");
                    _loginPage.LoginSuccess.ShouldBeFalse();
                }
            }
            catch (Exception e)
            {
                _logger.Fatal("-- Member Invalid Login Test: [EXCEPTION ENCOUNTERED] --");
                _logger.Info("-- Exception info: " + e.Message);
            }
        };

        It should_have_failed_to_log_in = () =>
        {

        };
    }
}
