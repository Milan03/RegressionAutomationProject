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
    namespace MemberPortalTests
    {
        [Subject(typeof(LoginPage))]
        public class When_member_logs_in_with_valid_creds : BaseDriverTest
        {
            static LoginPage _loginPage;
            static Exception _expectedExcep;

            Establish context = () =>
                {
                    LoadDriver();
                    _logger.Info("-- Member Valid Login Test Initiating --");
                    _loginPage = new LoginPage(_driver, "member");
                };

            Because of = () => _expectedExcep = Catch.Exception(
                            () => _loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS));


            It should_have_successfully_logged_in = () =>
            {
                _expectedExcep.ShouldBeNull();
                _logger.Info("-- Member Valid Login Test: [PASSED] --");
            };
        }
    }
}
