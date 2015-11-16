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
    public class When_member_logs_in_with_invalid_creds : BaseDriverTest
    {
        static LoginPage _loginPage;

        Establish context = () =>
            {
                LoadDriver();
                _loginPage = new LoginPage(_driver, "member");
            };

        Because of = () =>
           _loginPage.SubmitLoginExpectingFailure();


        It should_have_failed_to_log_in = () =>
            {
                var error = HelperMethods.FindElement(_driver, "classname", "login-error-message");
                error.Text.ShouldEqual(Constants.LOGIN_ERROR_MSG);
            };
    }
}
