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
    public class When_member_logs_in_with_valid_creds : BaseDriverTest
    {
        static LoginPage _loginPage;

        Establish context = () =>
            {
                _driver = new FirefoxDriver();
                _loginPage = new LoginPage(_driver, "member");
            };

        Because of = () =>
            _driver.Navigate().GoToUrl("http://portal.test-web01.lbmx.com/login?redirect=%2f");


        It should_have_successfully_logged_in = () =>
            Catch.Exception(() => _loginPage.LoginAs(Constants.MEM_PORTAL_USER, Constants.MEM_PORTAL_PASS))
                .ShouldBeNull();
    }
}
